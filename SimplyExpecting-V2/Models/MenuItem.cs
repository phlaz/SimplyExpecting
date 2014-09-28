using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using FuturePrimitive;
using FuturePrimitive.Hierarchy;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SimplyExpecting_V2.Data;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace SimplyExpecting_V2.Models
{

	public interface IHierarchy<TNode> where TNode : IHierarchy<TNode>
	{
		List<TNode> Children { get; }

		TNode Parent { get; }
	}

	/// <summary>
	/// Represents a menu item hierarchy to be displayed by the page
	/// </summary>
	[DataContract]
	public class MenuItem : IHierarchy<MenuItem>
	{
		public MenuItem Parent { get; set; }

		[DataMember]
		public int? ParentId { get; set; }

		[DataMember]
		public List<MenuItem> Children { get; } = new List<MenuItem>();

		[DataMember]
		public int Id { get; set; }

		public Content Content { get; set; }

		[DataMember]
		public int? ContentId { get; set; }

		[DataMember]
		public string Title { get; set; }

		[DataMember]
		public string Caption { get; set; }

		public bool IsEnabled { get; set; } = true;

		public MenuItem Insert(MenuItem child)
		{
			throw new NotImplementedException();
		}

		public MenuItem Delete(MenuItem child)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads the menu hierarchy asynchronously from the database store.
		/// </summary>
		/// <returns>Task containing the hierarchy as a JSON string.</returns>
		public static Task<string> ReadFromStoreAsync()
		{
			using (var db = new SimplyExpectingDataContext())
			{
				return db.MenuItems.Include("Content").GetTopLevelMenus()/*.RemoveCyclicReferences()*/.SerializeToJsonAsync();
			}
		}

		/// <summary>
		/// Sets all Parent references in the MenuItem and MenuItem children to null. This is to facilitate serializing the hierarchy to JSON
		/// as cyclic references are not supported by the JsonSerializer
		/// </summary>
		/// <returns>The MenuItem</returns>
		public MenuItem RemoveParent()
		{
			Parent = null;
			foreach (var child in Children)
				child.RemoveParent();
			return this;
		}



	}
	static class MenuItemsExtensions
	{
		public static IEnumerable<MenuItem> GetTopLevelMenus(this IEnumerable<MenuItem> items)
		{
			return items.Where(i => i.Parent == null && i.IsEnabled);
		}

		public static IEnumerable<MenuItem> RemoveCyclicReferences(this IEnumerable<MenuItem> items)
		{
			var list = items.ToList();
			return list.Select(i => i.RemoveParent());
		}

		/// <summary>
		/// Serializes an enumeration of MenuItem objects to JSON
		/// </summary>
		/// <param name="items">The MenuItem objects to serialize</param>
		/// <returns>The JSON result string.</returns>
		public static Task<string> SerializeToJsonAsync(this IEnumerable<MenuItem> items)
		{
			var stringStream = new MemoryStream();
			new DataContractJsonSerializer(items.GetType()).WriteObject(stringStream, items.ToList());
			stringStream.Seek(0, SeekOrigin.Begin);
			using (var reader = new StreamReader(stringStream))
			{
				return reader.ReadToEndAsync();
			}
		}
	}
}
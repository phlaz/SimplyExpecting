using Newtonsoft.Json;
using SimplyExpecting_V2.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SimplyExpecting_V2.Models
{
	public static class SectionName
	{
		public const int Menu = 1;
		public const int Home = 2;
		public const int Pregnancy = 3;
		public const int  PostPregnancy = 4;
		public const int  HypnoBirthing = 5;
		public const int  FAQ = 6;
		public const int  Classes = 7;
		public const int  Contact = 8;
		public const int Testimonials = 9;
	}
	public static class DbContextExtensions
	{
		public static TContext NewContext<TContext>()
			where TContext : System.Data.Entity.DbContext, new()
		{
			using (var db = new TContext())
			{
				return db;
			}
		}
	}

	public interface IContent<out TResult>
	{
		TResult ReadFromStoreAsync(int sectionId, int version);
	} 

	[DataContract]
	public class Content : IContent<Task<Content>>
	{
		[DataMember(Order = 1),JsonProperty("Id")]
		public int Id { get; set; }

		[DataMember(Order = 2), JsonProperty("Version")]
		public int Version { get; set; }

		[ForeignKey("Section"), DataMember(Order = 3), JsonProperty("SectionId")]
		public int SectionId { get;set; }

		[ForeignKey("SectionId")]
		public ContentSection Section { get;set; }

		[DataMember(Order = 4), JsonProperty("Html")]
		public string Html { get;set; }

		public Task<Content> ReadFromStoreAsync(int sectionId, int version)
		{
			return Task.Run(() =>
			{
				using (var db = new SimplyExpectingDataContext())
				{
					var content = db.PageContent.Where(c => c.SectionId == sectionId && c.Version > version)
									  		    .OrderByDescending(c => c.Version)
											    .FirstOrDefault();

					if (content == null)
						content = new Content { SectionId = sectionId, Version = version };

					return content;
				}
			});
		}


		public Task WriteToStore()
		{
			return Task.Run(() =>
			{
				try
				{
					using (var db = new SimplyExpectingDataContext())
					{
						var lastVersion = db.PageContent.OrderByDescending(c => c.Version).FirstOrDefault().Version;
						Version = lastVersion + 1;
						db.PageContent.Add(this);
						db.SaveChanges();
					}
				}
				catch(Exception x)
				{
					System.Diagnostics.Debug.WriteLine(x.Message);
				}
			});
		}
	}

	public class MenuContent : Content { }

}
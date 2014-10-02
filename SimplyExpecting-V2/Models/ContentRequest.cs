using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyExpecting_V2.Models
{
	public class ContentRequest : IMessage
	{
		public int SectionId { get; set; }

		public int Version { get; set; }
	}
}
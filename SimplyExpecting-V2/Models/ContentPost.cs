using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimplyExpecting_V2.Models
{
	interface IMessage
	{
	}

	public class ContentPost : IMessage
	{
		public Content Content { get; set; }
	}
}
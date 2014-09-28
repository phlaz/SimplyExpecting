using SimplyExpecting_V2.Data;
using SimplyExpecting_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimplyExpecting_V2.Controllers
{
	[RoutePrefix("api/menu")]
	public class MenuController : ApiController
	{

		[Route("{version:int}")]
		public Task<Content> Get(int version)
		{
			return Task.Run(() =>
			{
				var content = SimplyExpecting_V2.Models.Content.ReadFromStoreAsync("Menu", version);
				content.Result.Html = MenuItem.ReadFromStoreAsync().Result;
				return content;
			});
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}
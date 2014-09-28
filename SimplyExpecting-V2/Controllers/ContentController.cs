using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using simplyExpecting = SimplyExpecting_V2.Models;

namespace SimplyExpecting_V2.Controllers
{
	[RoutePrefix("api/content")]
	public class ContentController : ApiController
	{
		/// <summary>
		///	Initiates the request to open a websocket connection.
		/// </summary>
		/// <returns></returns>
		[RequireHttps]
		public HttpResponseMessage Get()
		{
			HttpContext.Current.AcceptWebSocketRequest(new SocketHandler());
			return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
		}

		[RequireHttps]
		[Route("{sectionId:int}/{version:int}")]
		public Task<simplyExpecting.Content> Get(int sectionId, int version)
		{
			return sectionId == 1? GetMenu(version) : new simplyExpecting.Content().ReadFromStoreAsync(sectionId, version);
		}

		[RequireHttps]
		[Route("{version:int}")]
		public Task<simplyExpecting.Content> GetMenu(int version)
		{
			var content = new simplyExpecting.MenuContent().ReadFromStoreAsync(simplyExpecting.SectionName.Menu, version);
			var menu = simplyExpecting.MenuItem.ReadFromStoreAsync();
			Task.WaitAll(content, menu);
			if (content.Result != null)
			content.Result.Html = menu.Result;
			return content;
		}

		[RequireHttps]
		[HttpPost]
		public IHttpActionResult Post(simplyExpecting.Content content)
		{
			content.WriteToStore();
			return Ok(content);
		}
	}

	internal class SocketHandler : WebSocketHandler
	{
		internal static WebSocketCollection _connections = new WebSocketCollection();

		DateTime _lastCommunication;

		public static void BroadcastNewContent(simplyExpecting.Content content)
		{
			_connections.Broadcast(JsonConvert.SerializeObject(content));
		}

		public override void OnOpen()
		{
			_lastCommunication = DateTime.Now;
			_connections.Add(this);
		}

		public override void OnMessage(string message)
		{
			_lastCommunication = DateTime.Now;
			var values = message.Split(':');
			int.TryParse(values[0], out int page);
			int.TryParse(values[1], out int version);
			simplyExpecting.Content content = null;
			switch (page)
			{
				case simplyExpecting.SectionName.Menu:
					content = new ContentController().GetMenu(version).Result;
					break;

				default:
					content = new ContentController().Get(int.Parse(values[0]), int.Parse(values[1])).Result;
					break;
			}
			
			Send(JsonConvert.SerializeObject(content));
		}

		public override void OnClose()
		{
			base.OnClose();
			_connections.Remove(this);
		}
	}
}

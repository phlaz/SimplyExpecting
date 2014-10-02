using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using simplyExpecting = SimplyExpecting_V2.Models;
using SimplyExpecting_V2.Models;
using System.Runtime.Serialization;

namespace SimplyExpecting_V2.Controllers
{
	[RoutePrefix("api/content")]
	public class ContentController : ApiController
	{
		/// <summary>
		///	Initiates the request to open a websocket connection.
		/// </summary>
		/// <returns></returns>
		public HttpResponseMessage Get()
		{
			HttpContext.Current.AcceptWebSocketRequest(new SocketHandler());
			return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
		}

		[Route("{sectionId:int}/{version:int}")]
		public Task<Content> Get(int sectionId, int version)
		{
			return sectionId == 1 ? GetMenu(version) : new simplyExpecting.Content().ReadFromStoreAsync(sectionId, version);
		}

		[Route("{version:int}")]
		public Task<Content> GetMenu(int version)
		{
			var content = new simplyExpecting.MenuContent().ReadFromStoreAsync(simplyExpecting.SectionName.Menu, version);
			var menu = simplyExpecting.MenuItem.ReadFromStoreAsync();
			Task.WaitAll(content, menu);
			if (content.Result != null)
			content.Result.Html = menu.Result;
			return content;
		}

		[HttpPost]
		public Content Post(simplyExpecting.Content content)
		{
			content.WriteToStore();
			return content;
		}
	}

	internal class SocketHandler : WebSocketHandler
	{
		[DataContract]
		internal class WebSocketMessage
		{
			[DataMember(Order =1), JsonProperty("Message")]
			public string Message { get; set; }

			[DataMember(Order = 2), JsonProperty("Content")]
			public Content Content { get; set; }
		}

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
			var msg = JsonConvert.DeserializeObject<WebSocketMessage>(message);

			if (msg.Message ==  "ContentRequest")
				Send(ProcessRequestMessage(msg.Content));

			if (msg.Message == "ContentPost")
				ProcessPostMessage(msg.Content);
		}

		private void ProcessPostMessage(Content content)
		{
			content.WriteToStore().ContinueWith(t => BroadcastNewContent(content));
		}

		private string ProcessRequestMessage(Content content)
		{
			_lastCommunication = DateTime.Now;
			int page = content.SectionId;
			int version = content.Version;
			switch (page)
			{
				case simplyExpecting.SectionName.Menu:
					//content = new ContentController().GetMenu(version).Result;
					break;

				default:
					//content = new ContentController().Get(page, version).Result;
					break;
			}

			return JsonConvert.SerializeObject(content);
		}

		public override void OnClose()
		{
			base.OnClose();
			_connections.Remove(this);
		}
	}
}

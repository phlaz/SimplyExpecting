using Microsoft.Web.WebSockets;
using Newtonsoft.Json;
using SimplyExpecting_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SimplyExpecting_V2.Controllers
{
	public class PushController : ApiController
	{
		// GET api/<controller>
		public HttpResponseMessage Get()
		{
			HttpContext.Current.AcceptWebSocketRequest(new SocketHandler());
			return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
		}

		internal class SocketHandler : WebSocketHandler
		{
			internal static WebSocketCollection _connections = new WebSocketCollection();

			DateTime _lastCommunication;

			public static void BroadcastNewContent(Content content)
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
				var values = message.Split(':');
				var content = new ContentController().Get(int.Parse(values[0]), int.Parse(values[1])).Result;
				Send(JsonConvert.SerializeObject(content));
			}

			public override void OnClose()
			{
				base.OnClose();
				_connections.Remove(this);
			}
			public override void OnError()
			{
				//HttpContext.Current.CurrentNotification.ToString();
				base.OnError();

			}
		}
	}
}
﻿using SimplyExpecting_V2.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Routing;

namespace SimplyExpecting_V2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, version = RouteParameter.Optional  }
            );

			config.Formatters.Add(new BsonMediaTypeFormatter());

			Database.SetInitializer(new DatabaseInitialier());
			using (var db = new SimplyExpectingDataContext())
			{
				db.Configuration.LazyLoadingEnabled = true;
				db.Database.Initialize(true);
			}
		}
    }
}


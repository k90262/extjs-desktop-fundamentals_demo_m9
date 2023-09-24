using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using WebApp.Models;

namespace WebApp
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
                defaults: new { id = RouteParameter.Optional }
            );

            // Set the initializer here
            //Database.SetInitializer(new SeDbInitializer());
            // Ref. 
            // 1. https://stackoverflow.com/questions/30154283/entity-framework-database-connection-error
            // 2. https://learn.microsoft.com/zh-tw/previous-versions/visualstudio/visual-studio-2015/data-tools/upgrade-dot-mdf-files?view=vs-2015&redirectedfrom=MSDN

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.Indent = true;

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();


            // Now initialize it
            using (var context = new SeContext())
            {
                context.Database.Initialize(false);

                var cnt = context.Sessions.Count();

            }
        }
    }
}

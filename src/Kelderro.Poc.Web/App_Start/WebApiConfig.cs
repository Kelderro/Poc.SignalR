using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Kelderro.Poc.Data;
using Kelderro.Poc.Web.Filters;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace Kelderro.Poc.Web
{
	/// <summary>
	/// Web API configuration and services
	/// </summary>
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API routes
			config.MapHttpAttributeRoutes();

			//config.Routes.MapHttpRoute(
			//		name: "DefaultApi",
			//		routeTemplate: "api/{controller}/{id}",
			//		defaults: new { id = RouteParameter.Optional }
			//);

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			var container = new UnityContainer();
			container.RegisterType<IUnitOfWork, UnitOfWork<PocContext>>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);

			// Configure HTTP Caching using Entity Tags (ETags)
			var cacheCowCacheHandler = new CacheCow.Server.CachingHandler();
			config.MessageHandlers.Add(cacheCowCacheHandler);

			// Enforce HTTPS filter over the entire Web API
			// config.Filters.Add(new ForceHttpsAttribute());

			// Support for CORS
			var corsAttribute = new EnableCorsAttribute("*", "*", "GET,POST");
			config.EnableCors(corsAttribute);
		}
	}
}

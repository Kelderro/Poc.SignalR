﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace Kelderro.Poc.Web.Filters
{
	public class ForceHttpsAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			var request = actionContext.Request;

			if (request.RequestUri.Scheme == Uri.UriSchemeHttps)
				return;

			const string html = "<p>Https is required</p>";

			if (request.Method.Method == "GET")
			{
				actionContext.Response = request.CreateResponse(HttpStatusCode.Found);
				actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

				var httpsNewUri = new UriBuilder(request.RequestUri)
				{
					Scheme = Uri.UriSchemeHttps,
					Port = 443
				};

				actionContext.Response.Headers.Location = httpsNewUri.Uri;
			}
			else
			{
				actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound);
				actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
			}
		}
	}
}
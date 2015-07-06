﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Filters;
using Kelderro.Poc.Data;

namespace Kelderro.Poc.Web.Filters
{
	public class PocAuthorizeAttribute : AuthorizationFilterAttribute
	{
		//[Inject]
		public IUnitOfWork UnitOfWork { get; set; }

		public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			//Case that user is authenticated using forms authentication
			//so no need to check header for basic authentication.
			if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
			{
				return;
			}

			var authHeader = actionContext.Request.Headers.Authorization;

			if (authHeader != null)
			{
				if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
				!string.IsNullOrWhiteSpace(authHeader.Parameter))
				{
					var credArray = GetCredentials(authHeader);
					var userName = credArray[0];
					var password = credArray[1];

					if (IsResourceOwner(userName, actionContext))
					{
						//You can use Websecurity or asp.net memebrship provider to login, for
						//for he sake of keeping example simple, we used out own login functionality
						//if (UnitOfWork.GetRepository<User>().Fetch().SingleOrDefault() .LoginStudent(userName, password))
						////{
							var currentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
							Thread.CurrentPrincipal = currentPrincipal;
							return;
						//}
					}
				}
			}

			HandleUnauthorizedRequest(actionContext);
		}

		private string[] GetCredentials(System.Net.Http.Headers.AuthenticationHeaderValue authHeader)
		{

			//Base 64 encoded string
			var rawCred = authHeader.Parameter;
			var encoding = Encoding.GetEncoding("iso-8859-1");
			var cred = encoding.GetString(Convert.FromBase64String(rawCred));

			var credArray = cred.Split(':');

			return credArray;
		}

		private static bool IsResourceOwner(string userName, System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			var routeData = actionContext.Request.GetRouteData();
			var resourceUserName = routeData.Values["userName"] as string;

			return resourceUserName == userName;
		}

		private static void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
			actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='PoC' location='http://localhost:8323/account/login'");
		}
	}
}
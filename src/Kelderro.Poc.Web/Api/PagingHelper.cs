using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Kelderro.Poc.Web.Api
{
	public static class PagingHelper
	{
		public static IEnumerable<TViewModel> Create<TViewModel, TEntity>(HttpRequestMessage request, Func<TEntity, TViewModel> viewModelMapper, IOrderedEnumerable<TEntity> query, string routeName, int page = 0, int pageSize = 10) where TViewModel : class 
			where TEntity : class
		{
			var totalCount = query.Count();
			var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

			var urlHelper = new UrlHelper(request);
			var prevLink = page > 0 ? urlHelper.Link(routeName, new { page = page - 1, pageSize }) : "";
			var nextLink = page < totalPages - 1 ? urlHelper.Link(routeName, new { page = page + 1, pageSize }) : "";

			var paginationHeader = new
			{
				TotalCount = totalCount,
				TotalPages = totalPages,
				PrevPageLink = prevLink,
				NextPageLink = nextLink
			};

			System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination",
				Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

			var results = query
				.Skip(pageSize * page)
				.Take(pageSize)
				.ToList()
				.Select(viewModelMapper);

			return results;
		}
	}
}
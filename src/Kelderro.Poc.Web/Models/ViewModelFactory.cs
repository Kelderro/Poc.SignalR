using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using AutoMapper;
using Kelderro.Poc.Data;
using Kelderro.Poc.Data.Entities;
using WebGrease.Css.Extensions;

namespace Kelderro.Poc.Web.Models
{
	public class ViewModelFactory
	{
		private readonly UrlHelper _urlHelper;
		private readonly IUnitOfWork _unitOfWork;

		public ViewModelFactory(HttpRequestMessage request, IUnitOfWork unitOfWork)
		{
			_urlHelper = new UrlHelper(request);
			_unitOfWork = unitOfWork;
		}

		public void Create(bool test)
		{
			
		}

		public UserViewModel Create(User user)
		{
			var userViewModel = Mapper.Map<UserViewModel>(user);
			//userViewModel.Url = _urlHelper.Link("User", new {id = user.Id});
			return userViewModel;
		}

		public User Parse(UserViewModel userViewModel)
		{
			try
			{
				if (userViewModel == null)
					return null;

				var user = Mapper.Map<User>(userViewModel);
				_unitOfWork.GetRepository<Message>().Fetch()
																					  .Where(x => x.User.Id == user.Id)
																						.ForEach(x => user.Messages.Add(x));

				return user; 
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
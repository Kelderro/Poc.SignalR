using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kelderro.Poc.Data;
using Kelderro.Poc.Data.Entities;
using Kelderro.Poc.Web.Models;

namespace Kelderro.Poc.Web.Api
{
	// Enforce HTTPS on the entire controller
	//[Filters.ForceHttps]
	[RoutePrefix("api/users")]
	public class UserController : BaseApiController
	{
		private readonly IGenericRepository<User> _userRepository;

		public UserController(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			_userRepository = unitOfWork.GetRepository<User>();
		}

		[Route(Name = "UsersRoute")]
		public IEnumerable<UserViewModel> Get(int page = 0, int pageSize = 10)
		{
			var query = _userRepository.GetAll().OrderBy(c => c.Id);

			return PagingHelper.Create(Request, x => TheViewModelFactory.Create(x), query, "UsersRoute", page, pageSize);

		}

		[Route("{id:int}")]
		public IHttpActionResult GetUser(int id)
		{
			try
			{
				var user = _userRepository.Fetch().SingleOrDefault(x => x.Id == id);
				if (user != null)
				{
					return Ok(TheViewModelFactory.Create(user));
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return InternalServerError(ex);
			}
		}

		// Enforce HTTPS on POST method only
		//[Filters.ForceHttps]
		public HttpResponseMessage Post([FromBody] UserViewModel userViewModel)
		{
			try
			{
				var entity = TheViewModelFactory.Parse(userViewModel);

				if (entity == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read user from body");

				entity.RegistrationDate = DateTime.UtcNow;

				if (_userRepository.Add(entity) && UnitOfWork.SaveAll())
				{
					return Request.CreateResponse(HttpStatusCode.Created, TheViewModelFactory.Create(entity));
				}

				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}

		[HttpPatch]
		[HttpPut]
		public HttpResponseMessage Put(int id, [FromBody] UserViewModel userViewModel)
		{
			try
			{
				var updatedUser = TheViewModelFactory.Parse(userViewModel);

				if (updatedUser == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not read user from body");

				var originalUser = _userRepository.Fetch().SingleOrDefault(x => x.Id == id);

				if (originalUser == null || originalUser.Id != id)
				{
					return Request.CreateResponse(HttpStatusCode.NotModified, "User is not found");
				}

				updatedUser.Id = id;

				if (_userRepository.Update(originalUser, updatedUser) && UnitOfWork.SaveAll())
				{
					return Request.CreateResponse(HttpStatusCode.OK, TheViewModelFactory.Create(updatedUser));
				}

				return Request.CreateResponse(HttpStatusCode.NotModified);
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}

		public HttpResponseMessage Delete(int id)
		{
			try
			{
				var user = _userRepository.Fetch().SingleOrDefault(x => x.Id == id);

				if (user == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound);
				}

				if (_userRepository.Delete(User) && UnitOfWork.SaveAll())
				{
					return Request.CreateResponse(HttpStatusCode.OK);
				}
				return Request.CreateResponse(HttpStatusCode.BadRequest);
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
			}
		}
	}
}
using System.Web.Http;
using Kelderro.Poc.Data;
using Kelderro.Poc.Web.Models;

namespace Kelderro.Poc.Web.Api
{
	public abstract class BaseApiController : ApiController
	{
		protected readonly IUnitOfWork UnitOfWork;
		private ViewModelFactory _viewModelFactory;

		protected BaseApiController(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		protected ViewModelFactory TheViewModelFactory
		{
			get { return _viewModelFactory ?? (_viewModelFactory = new ViewModelFactory(Request, UnitOfWork)); }
		}
	}
}
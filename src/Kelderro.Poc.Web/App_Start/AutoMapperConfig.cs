using Kelderro.Poc.Data.Entities;
using Kelderro.Poc.Web.Models;

namespace Kelderro.Poc.Web
{
	public class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			AutoMapper.Mapper.CreateMap<User, UserViewModel>().ReverseMap();
		}
	}
}
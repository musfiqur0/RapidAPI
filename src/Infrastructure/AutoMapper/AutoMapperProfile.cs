using Application.DTOs;
using AutoMapper;
using Domain.Model;

namespace Infrastructure.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<EmployeeAddEditDto, Employee>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
		}
	}
}

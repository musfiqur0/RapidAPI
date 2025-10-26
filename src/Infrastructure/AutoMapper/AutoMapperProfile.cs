using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<CandidateSelectionAddEditDto, CandidateSelection>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<IncrementAddEditDto, Increment>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<PreAlertAddEditDto, PreAlert>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<JobPostAddEditDto, JobPost>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<PaymentModeAddEditDto, PaymentMode>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<GroupAddEditDto, Group>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<EmployeeAddEditDto, Employee>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
		}
	}
}

using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.AutoMapper
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<LeaveApplicationAddEditDto, LeaveApplication>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<BonusAddEditDto, Bonus>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<OnboardingAddEditDto, Onboarding>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<DeductionAddEditDto, Deduction>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<AllowanceAddEditDto, Allowance>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<TerminationAddEditDto, Termination>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
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

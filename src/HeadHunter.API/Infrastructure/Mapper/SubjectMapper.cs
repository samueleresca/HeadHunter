using AutoMapper;
using HeadHunter.API.Infrastructure.Requests.Service;
using HeadHunter.API.Infrastructure.Responses;
using HeadHunter.API.Models;

namespace HeadHunter.API.Infrastructure.Mapper
{
	internal class SubjectMapper : Profile
	{
		public SubjectMapper()
		{
			CreateMap<Subject, SubjectResponseModel>();
			CreateMap<SubjectResponseModel, Subject>();

			CreateMap<CreateSubjectRequest, Subject>();
			CreateMap<UpdateSubjectRequest, Subject>();
		}
	}
}
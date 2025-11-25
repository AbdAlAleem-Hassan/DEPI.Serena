

using Serena.BLL.Models.PatientDoctorReviews;

namespace Serena.BLL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDtoGet>().ReverseMap();
            CreateMap<PatientDetailsDTO, Patient>();
            CreateMap<CreateAndUpdatePatientDTO, Patient>().ReverseMap();


            CreateMap<PatientDoctorReview, PatientDoctorReviewGetDTO>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.comment))
                .ReverseMap();

            CreateMap<PatientDoctorReviewCreateUpdateDTO, PatientDoctorReview>()
                .ForMember(dest => dest.comment, opt => opt.MapFrom(src => src.Comment));
        }
    }

}

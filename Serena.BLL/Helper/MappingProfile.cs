

using Serena.BLL.Models.Appointements;
using Serena.BLL.Models.DoctorHospitalReviews;
using Serena.BLL.Models.DoctorService;
using Serena.BLL.Models.PatientDoctorReviews;
using Serena.BLL.Models.PatientHospitalReviews;
using Serena.BLL.Models.Schedules;

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

            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<CreateAndUpdateAppointmentDTO, Appointment>().ReverseMap();

            CreateMap<Service, ServiceDTO>()
           .ForMember(dest => dest.DoctorName, opt =>
               opt.MapFrom(src => src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : null));

            CreateMap<CreateAndUpdateServiceDTO, Service>();

            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.DoctorName,
                           opt => opt.MapFrom(src => src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : null));

           
            CreateMap<CreateAndUpdateScheduleDTO, Schedule>();

            CreateMap<PatientHospitalReview, PatientHospitalReviewDTO>()
               .ForMember(dest => dest.PatientName,
                   opt => opt.MapFrom(src => src.Patient != null ? $"{src.Patient.FirstName} {src.Patient.LastName}" : null))
               .ForMember(dest => dest.HospitalName,
                   opt => opt.MapFrom(src => src.Hospital != null ? src.Hospital.Name : null));

            CreateMap<CreateAndUpdatePatientHospitalReviewDTO, PatientHospitalReview>();

            CreateMap<DoctorHospitalReview, DoctorHospitalReviewDTO>()
                .ForMember(dest => dest.DoctorName,
                    opt => opt.MapFrom(src =>
                        src.Doctor != null ? $"{src.Doctor.FirstName} {src.Doctor.LastName}" : null))
                .ForMember(dest => dest.HospitalName,
                    opt => opt.MapFrom(src => src.Hospital != null ? src.Hospital.Name : null));

            CreateMap<DoctorHospitalReviewCreateUpdateDTO, DoctorHospitalReview>();
        }
    }

}

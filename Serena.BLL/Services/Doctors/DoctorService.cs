using Microsoft.EntityFrameworkCore;
using Serena.BLL.Common.Services.Attachments;
using Serena.BLL.Models.Doctors;
using Serena.BLL.Models.Schedules;
using Serena.DAL.Entities;
using Serena.DAL.Persistence.UnitOfWork;

namespace Serena.BLL.Services.Doctors
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachment;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IAttachmentService attachment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attachment = attachment;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.DoctorRepository.GetIQueryable()
                .Include(d => d.Department).ThenInclude(dd => dd.Hospital)
                .Select(
                doctor => new DoctorDTO
                {
                    Id = doctor.Id,
                    UserId = doctor.UserId,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Image = doctor.ImageUrl,
                    Rank = doctor.Rank,
                    Specialization = doctor.Specialization,
                    YearsOfExperience = doctor.YearsOfExperience,
                    Department = doctor.Department.Name,
                    Hospital = doctor.Department.Hospital.Name
                }).AsNoTracking().ToListAsync();
            return doctors;
        }

        public async Task<DoctorDetailsDTO?> GetDoctorByIdAsync(int doctorId)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetAsync(doctorId);
            if (doctor == null)
            {
                return null;
            }

            var doctorDTO = new DoctorDetailsDTO
            {
                Id = doctor.Id,
                UserId = doctor.UserId,
                FirstName = doctor.FirstName,
                MiddleName = doctor.MiddleName,
                LastName = doctor.LastName,
                Image = doctor.ImageUrl,
                Rank = doctor.Rank,
                YearsOfExperience = doctor.YearsOfExperience,
                City = doctor.City,
                Street = doctor.Street,
                Country = doctor.Country,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Gender = doctor.Gender,
                DateOfBirth = doctor.DateOfBirth,
                MaritalStatus = doctor.MaritalStatus,
                Specialization = doctor.Specialization,
                SubSpecialization = doctor.SubSpecialization,
                LicenseNumber = doctor.LicenseNumber,
                NationalID = doctor.NationalID,
                ZipCode = doctor.ZipCode,
                DepartmentId = doctor.DepartmentId,
                HospitalId = doctor.HospitalId,
                HospitalAddressId = doctor.HospitalAddressId,
                Schedules = new List<ScheduleDTO>()
            };
            foreach(Schedule schedule in doctor.Schedules)
            {
                doctorDTO.Schedules.Add(new ScheduleDTO
                {
                    Id = schedule.Id,
                    Date = schedule.Date,
                    Price = schedule.Price,
                    IsAvailable = schedule.IsAvailable,
                });
            }
            return doctorDTO;
        }

        public async Task<int> CreateDoctorAsync(CreateAndUpdateDoctorDTO doctorDto)
        {
            var doctor = new Doctor
            {
                UserId = doctorDto.UserId,
                FirstName = doctorDto.FirstName,
                MiddleName = doctorDto.MiddleName,
                LastName = doctorDto.LastName,
                ImageUrl = doctorDto.Image != null ? await _attachment.UploadAsync(doctorDto.Image, "Imgs") : null,
                Rank = doctorDto.Rank,
                YearsOfExperience = doctorDto.YearsOfExperience,
                City = doctorDto.City,
                Street = doctorDto.Street,
                Country = doctorDto.Country,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                Gender = doctorDto.Gender,
                DateOfBirth = doctorDto.DateOfBirth,
                MaritalStatus = doctorDto.MaritalStatus,
                Specialization = doctorDto.Specialization,
                SubSpecialization = doctorDto.SubSpecialization,
                LicenseNumber = doctorDto.LicenseNumber,
                DepartmentId = doctorDto.DepartmentId,
                HospitalId = doctorDto.HospitalId,
                NationalID = doctorDto.NationalID,
                ZipCode = doctorDto.ZipCode,
                HospitalAddressId = doctorDto.HospitalAddressId,

            };

            _unitOfWork.DoctorRepository.Add(doctor);
            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.CompleteAsync();

        }

        public async Task<int> UpdateDoctorAsync(int id, CreateAndUpdateDoctorDTO doctorDto)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetAsync(id);

            doctor.FirstName = doctorDto.FirstName;
            doctor.MiddleName = doctorDto.MiddleName;
            doctor.LastName = doctorDto.LastName;
            doctor.Rank = doctorDto.Rank;
            doctor.YearsOfExperience = doctorDto.YearsOfExperience;
            doctor.City = doctorDto.City;
            doctor.Street = doctorDto.Street;
            doctor.Country = doctorDto.Country;
            doctor.Email = doctorDto.Email;
            doctor.PhoneNumber = doctorDto.PhoneNumber;
            doctor.Gender = doctorDto.Gender;
            doctor.DateOfBirth = doctorDto.DateOfBirth;
            doctor.MaritalStatus = doctorDto.MaritalStatus;
            doctor.Specialization = doctorDto.Specialization;
            doctor.SubSpecialization = doctorDto.SubSpecialization;
            doctor.LicenseNumber = doctorDto.LicenseNumber;
            doctor.DepartmentId = doctorDto.DepartmentId;
            doctor.HospitalId = doctorDto.HospitalId;
            doctor.ZipCode = doctorDto.ZipCode;
            if (doctorDto.Image != null)
            {
                doctor.ImageUrl = await _attachment.UploadAsync(doctorDto.Image, "Imgs");
            }

            _unitOfWork.DoctorRepository.Update(doctor);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteDoctorAsync(int id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetAsync(id);
            if (doctor == null)
            {
                return 0;
            }
            _unitOfWork.DoctorRepository.Delete(doctor);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<DoctorDetailsDTO?> GetDoctorByUserIdAsync(string id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetIQueryable()
                .Where(d => d.UserId == id)
                .FirstOrDefaultAsync();
            var result = new DoctorDetailsDTO()
            {
                City = doctor.City,
                Country = doctor.Country,
                Id = doctor.Id,
                UserId = id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                MiddleName = doctor.MiddleName,
                MaritalStatus = doctor.MaritalStatus,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Gender = doctor.Gender,
                DateOfBirth = doctor.DateOfBirth,
                Specialization = doctor.Specialization,
                SubSpecialization = doctor.SubSpecialization,
                Rank = doctor.Rank,
                LicenseNumber = doctor.LicenseNumber,
                NationalID = doctor.NationalID,
                YearsOfExperience = doctor.YearsOfExperience,
                Street = doctor.Street,
                ZipCode = doctor.ZipCode,
                Image = doctor.ImageUrl

            };

            return result;

        }
        public async Task<List<DoctorDetailsDTO?>> FilterDoctors(QueryParams filter)
        {
            var query = _unitOfWork.DoctorRepository.GetIQueryable().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(d => (d.FirstName + " " + d.LastName).Contains(filter.Name));
            }

            if (!string.IsNullOrEmpty(filter.Specialization))
            {
                query = query.Where(d => d.Specialization == filter.Specialization);
            }
            if (!string.IsNullOrEmpty(filter.City))
            {
                query = query.Where(d => d.City == filter.City);
            }
            if (filter.YearsOfExperience > 0)
            {
                query = query.Where(d => d.YearsOfExperience >= filter.YearsOfExperience);
            }
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "name_asc":
                        query = query.OrderBy(d => d.FirstName).ThenBy(d => d.LastName);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(d => d.FirstName).ThenByDescending(d => d.LastName);
                        break;
                    case "experience_asc":
                        query = query.OrderBy(d => d.YearsOfExperience);
                        break;
                    case "experience_desc":
                        query = query.OrderByDescending(d => d.YearsOfExperience);
                        break;
                    default:
                        break;
                }


            }
            var doctors = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var results = _mapper.Map<List<DoctorDetailsDTO?>>(doctors);

            return results;
        }
        public async Task<List<Appointment?>> GetPatientForDoctor(int id)
        {
            var result = await _unitOfWork.AppointmentRepository.GetIQueryable()
                .Where(x => x.DoctorId == id).
                 Include(x => x.Patient)
                .Include(x=>x.Schedule).ToListAsync();
            return result;
        }
    }
}

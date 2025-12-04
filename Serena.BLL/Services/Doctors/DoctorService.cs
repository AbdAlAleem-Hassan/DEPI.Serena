using Microsoft.EntityFrameworkCore;
using Serena.BLL.Common.Services.Attachments;
using Serena.BLL.Models.Doctors;
using Serena.DAL.Entities;
using Serena.DAL.Persistence.UnitOfWork;

namespace Serena.BLL.Services.Doctors
{
	public class DoctorService : IDoctorService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAttachmentService _attachment;

		public DoctorService(IUnitOfWork unitOfWork, IAttachmentService attachment)
		{
			_unitOfWork = unitOfWork;
			_attachment = attachment;
		}

		public async Task<IEnumerable<DoctorDTO>> GetAllDoctorsAsync()
		{
			var doctors = await _unitOfWork.DoctorRepository.GetIQueryable()
				.Include(d => d.Department).ThenInclude(dd => dd.Hospital)
				.Select(
				doctor => new DoctorDTO
				{
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

				Department = doctor.Department.Name,
				Hospital = doctor.Department.Hospital.Name,
				Services = doctor.Services.Select(s => s.Name).ToList(),
				//Languages = doctor.Languages.Select(l => l.LanguageName).ToList(),
				PatientDoctorReviews = doctor.PatientDoctorReviews.ToList(),
			};
			return doctorDTO;
		}

		public async Task<int> CreateDoctorAsync(CreateAndUpdateDoctorDTO doctorDto)
		{
			var doctor = new Doctor
			{
				UserId=doctorDto.UserId,
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

		public async Task<int> UpdateDoctorAsync(int id,CreateAndUpdateDoctorDTO doctorDto)
		{
			var doctor = await _unitOfWork.DoctorRepository.GetAsync(id);
			
				doctor.FirstName = doctorDto.FirstName;
				doctor.MiddleName = doctorDto.MiddleName;
				doctor.LastName = doctorDto.LastName;
				doctor.ImageUrl = doctorDto.Image != null ? await _attachment.UploadAsync(doctorDto.Image, "Imgs") : null;
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
				doctor.NationalID = doctorDto.NationalID;
				doctor.ZipCode = doctorDto.ZipCode;
				doctor.HospitalAddressId = doctorDto.HospitalAddressId;
			
			
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

	}
}

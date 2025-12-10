using Microsoft.EntityFrameworkCore;
using Serena.BLL.Common.Services.Attachments;
using Serena.BLL.Models.Addresses;
using Serena.BLL.Models.Departments;
using Serena.BLL.Models.Doctors;
using Serena.BLL.Models.Hospitals;
using System.Net.Mail;

namespace Serena.BLL.Services.Hospitals
{
	public class HospitalService : IHospitalService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAttachmentService _attachment;

		public HospitalService(IUnitOfWork unitOfWork, IAttachmentService attachment)
		{
			_unitOfWork = unitOfWork;
			_attachment = attachment;
		}

		public async Task<IEnumerable<HospitalDTO>> GetAllHospitalsAsync()
		{
			var hospitals = await _unitOfWork.HospitalRepository.GetIQueryable()
				.Include(h => h.Departments)
				.Include(h => h.PatientHospitalReviews)
				.Include(h => h.DoctorHospitalReviews)
				.Include(h => h.HospitalAddresses)
				.Select(h => new HospitalDTO
				{
					Id = h.Id,
					ImageUrl = h.ImageUrl,
					Name = h.Name,
					Rank = h.Rank,
					AverageCost = h.AverageCost,
					HospitalPhone = h.HospitalPhone,
					Address = h.HospitalAddresses.Select(ha => new AddressDTO
					{
						Street = ha.Street,
						City = ha.City,
						District = ha.District,
						Country = ha.Country,
						ZipCode = ha.ZipCode,
						Latitude = ha.Latitude,
						Longitude = ha.Longitude
					}).ToList(),
                    EmergencyPhone = h.EmergencyPhone,
					Departments = h.Departments.Select(d => new DepartmentDTO
					{
						Name = d.Name
					}).ToList(),
                    PatientsReviews = h.PatientHospitalReviews.Count,
					DoctorsReviews = h.DoctorHospitalReviews.Count
				}).AsNoTracking().ToListAsync();

			return hospitals;

		}

		public async Task<HospitalDetailsDTO?> GetHospitalByIdAsync(int HospitalId)
		{
			var hospital = await _unitOfWork.HospitalRepository.GetAsync(HospitalId);


			if (hospital is null)
				return null;

			var hospitalDTO = new HospitalDetailsDTO
			{
				Name = hospital.Name,
				Rank = hospital.Rank,
				HospitalPhone = hospital.HospitalPhone,
				EmergencyPhone = hospital.EmergencyPhone,
				AverageCost = hospital.AverageCost,
				PatientHospitalReviews = hospital.PatientHospitalReviews.ToList(),
				DoctorHospitalReviews = hospital.DoctorHospitalReviews.ToList()
			};

			return hospitalDTO;
		}

		public async Task<int> CreateHospitalAsync(CreateAndUpdateHospitalDTO HospitalDto)
		{
			// 1. Create Base Hospital
			var hospital = new Hospital
			{
				Name = HospitalDto.Name,
				Rank = HospitalDto.Rank,
				AverageCost = HospitalDto.AverageCost,
				HospitalPhone = HospitalDto.HospitalPhone,
				EmergencyPhone = HospitalDto.EmergencyPhone,
				ImageUrl = HospitalDto.Image != null ? await _attachment.UploadAsync(HospitalDto.Image, "hospitals") : null,
				HospitalAddresses = HospitalDto.Address?.Select(a => new HospitalAddress
				{
					Street = a.Street,
					City = a.City,
					District = a.District,
					Country = a.Country,
					ZipCode = a.ZipCode,
					Latitude = a.Latitude,
					Longitude = a.Longitude
				}).ToList(),
				Departments = HospitalDto.Department?.Select(d => new Department
				{
					Name = d.Name
				}).ToList()
			};

			_unitOfWork.HospitalRepository.Add(hospital);
			return await _unitOfWork.CompleteAsync();

		}


		public async Task<int> DeleteHospitalAsync(int id)
		{
			var hospital = await _unitOfWork.HospitalRepository.GetAsync(id);
			if (hospital is null)
				return 0;
			_unitOfWork.HospitalRepository.Delete(hospital);
			return await _unitOfWork.CompleteAsync();

		}

		public async Task<int> UpdateHospitalAsync(int id, CreateAndUpdateHospitalDTO HospitalDto)
		{
			var hospital = await _unitOfWork.HospitalRepository.GetAsync(id);
			if (hospital is null)
				return 0;

			hospital.Name = HospitalDto.Name;
			hospital.Rank = HospitalDto.Rank;
			hospital.AverageCost = HospitalDto.AverageCost;
			hospital.HospitalPhone = HospitalDto.HospitalPhone;
			hospital.EmergencyPhone = HospitalDto.EmergencyPhone;



			_unitOfWork.HospitalRepository.Update(hospital);

			return await _unitOfWork.CompleteAsync();
		}
		public async Task<List<HospitalDetailsDTO?>> FilterHospital(QueryParamsForHospital queryParams)
		{
			var hospitalsQuery = _unitOfWork.HospitalRepository.GetIQueryable()
				.Include(h => h.Departments)
				.Include(h => h.HospitalAddresses)
				.AsNoTracking();
			if (!string.IsNullOrEmpty(queryParams.Name))
			{
				hospitalsQuery = hospitalsQuery.Where(h => h.Name.Contains(queryParams.Name));
			}
			if (!string.IsNullOrEmpty(queryParams.Department))
			{
				hospitalsQuery = hospitalsQuery.Where(h => h.Departments.Any(d => d.Name.Contains(queryParams.Department)));
			}
			if (queryParams.MaxAverageCost.HasValue)
			{
				hospitalsQuery = hospitalsQuery.Where(h => h.AverageCost <= queryParams.MaxAverageCost.Value);
			}
			if (!string.IsNullOrEmpty(queryParams.City))
			{
				hospitalsQuery = hospitalsQuery.Where(h => h.HospitalAddresses.Any(a => a.City.Contains(queryParams.City)));
			}
			if (!string.IsNullOrEmpty(queryParams.Country))
			{
				hospitalsQuery = hospitalsQuery.Where(h => h.HospitalAddresses.Any(a => a.Country.Contains(queryParams.Country)));
			}
			if (!string.IsNullOrEmpty(queryParams.Rank))
			{
				hospitalsQuery = hospitalsQuery.Where(h => h.Rank == queryParams.Rank);
			}
			var filteredHospitals = await hospitalsQuery.Select(h => new HospitalDetailsDTO
			{
				Id = h.Id,
                Name = h.Name,
				Rank = h.Rank,
				AverageCost = h.AverageCost,
				HospitalPhone = h.HospitalPhone,
				EmergencyPhone = h.EmergencyPhone,
				Addresses = h.HospitalAddresses.Select(ha => new AddressDTO
				{
					Street = ha.Street,
					City = ha.City,
					District = ha.District,
					Country = ha.Country,
					ZipCode = ha.ZipCode,
					Latitude = ha.Latitude,
					Longitude = ha.Longitude
				}).ToList(),
				Departments = h.Departments.Select(d => new DepartmentDTO
				{
					Name = d.Name
				}).ToList()

			}).ToListAsync();

			return filteredHospitals;
        }
		public async Task<Hospital?> GetHospitalDetails(int id)
		{
			var result = await _unitOfWork.HospitalRepository.GetIQueryable()
				.Include(h => h.Doctors)
				.FirstOrDefaultAsync(h => h.Id == id);
			return result;
		}
	}
}

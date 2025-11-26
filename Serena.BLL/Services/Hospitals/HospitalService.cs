using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Hospitals;

namespace Serena.BLL.Services.Hospitals
{
	public class HospitalService : IHospitalService
	{
		private readonly IUnitOfWork _unitOfWork;

		public HospitalService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
					Name = h.Name,
					Rank = h.Rank,
					AverageCost = h.AverageCost,
					HospitalPhone = h.HospitalPhone,
					Addresses = h.HospitalAddresses.Select(ha => ha.City).ToList(),
					EmergencyPhone= h.EmergencyPhone,
					DepartmentsNumber = h.Departments.ToList().Count,
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
				Addresses = hospital.HospitalAddresses.Select(ha => $"{ha.Country}, {ha.City} ,{ha.Street}").ToList(),
				Departments = hospital.Departments.Select(d => d.Name).ToList(),
				PatientHospitalReviews = hospital.PatientHospitalReviews.ToList(),
				DoctorHospitalReviews = hospital.DoctorHospitalReviews.ToList()
			};

			return hospitalDTO; 
		}

		public async Task<int> CreateHospitalAsync(CreateAndUpdateHospitalDTO HospitalDto)
		{
			var hospital = new Hospital
			{
				Name = HospitalDto.Name,
				Rank = HospitalDto.Rank,
				AverageCost = HospitalDto.AverageCost,
				HospitalPhone = HospitalDto.HospitalPhone,
				EmergencyPhone = HospitalDto.EmergencyPhone,
			};

			_unitOfWork.HospitalRepository.Add(hospital);
			await _unitOfWork.CompleteAsync();

			_unitOfWork.HospitalAddressRepository.Add(new HospitalAddress
			{
				HospitalId = hospital.Id,
				City = HospitalDto.Address.City,
				Country = HospitalDto.Address.Country,
				District = HospitalDto.Address.District,
				Street = HospitalDto.Address.Street,
				ZipCode = HospitalDto.Address.ZipCode,
				Latitude = HospitalDto.Address.Latitude,
				Longitude = HospitalDto.Address.Longitude,
			});
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
	}
}

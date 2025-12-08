using Serena.BLL.Models.Hospitals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Hospitals
{
	public interface IHospitalService
	{
		Task<IEnumerable<HospitalDTO>> GetAllHospitalsAsync();
		Task<HospitalDetailsDTO?> GetHospitalByIdAsync(int HospitalId);
		Task<int> CreateHospitalAsync(CreateAndUpdateHospitalDTO HospitalDto);
		Task<int> UpdateHospitalAsync(int id, CreateAndUpdateHospitalDTO HospitalDto);
		Task<int> DeleteHospitalAsync(int id);
		Task<List<HospitalDetailsDTO?>> FilterHospital(QueryParamsForHospital queryParams);

    }
}

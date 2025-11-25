
namespace Serena.BLL.Services.Patients;

public interface IPatientService
{
    Task<IEnumerable<PatientDtoGet>> GetAllPatientsAsync();
    Task<PatientDetailsDTO?> GetPatientByIdAsync(int patientId);
    Task<CreateAndUpdatePatientDTO> CreatePatientAsync(CreateAndUpdatePatientDTO patientDto);
    Task UpdatePatientAsync(int id, CreateAndUpdatePatientDTO patientDto);
    Task DeletePatientAsync(int id);
}

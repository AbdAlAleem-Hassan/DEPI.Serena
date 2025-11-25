
using Microsoft.EntityFrameworkCore;

namespace Serena.BLL.Services.Patients;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateAndUpdatePatientDTO> CreatePatientAsync(CreateAndUpdatePatientDTO patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);

         _unitOfWork.PatientRepository.Add(patient);
         await _unitOfWork.CompleteAsync();

        return patientDto;
    }

    public async Task DeletePatientAsync(int id)
    {
        var patient = await _unitOfWork.PatientRepository.GetAsync(id);
        _unitOfWork.PatientRepository.Delete(patient!);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<PatientDetailsDTO?> GetPatientByIdAsync(int patientId)
    {
        var patient = await _unitOfWork.PatientRepository
                                .GetIQueryable()
                                .Where(p => p.Id == patientId)
                                .Include(p => p.PatientDoctorReviews)
                                .Include(p => p.PatientHospitalReviews)
                                .Include(p => p.Appointments)
                                .FirstOrDefaultAsync();

        return _mapper.Map<PatientDetailsDTO>(patient);
    }

    public async Task UpdatePatientAsync(int id, CreateAndUpdatePatientDTO patientDto)
    {
        var existing = await _unitOfWork.PatientRepository.GetAsync(id);

        if (existing == null) return;

        _mapper.Map(patientDto, existing);

        _unitOfWork.PatientRepository.Update(existing);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<PatientDtoGet>> GetAllPatientsAsync()
    {
        var patients =  await _unitOfWork.PatientRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<PatientDtoGet>>(patients);

    }
}

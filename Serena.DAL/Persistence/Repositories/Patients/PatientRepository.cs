
namespace Serena.DAL.Persistence.Repositories.Patients;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

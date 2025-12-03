using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Schedules
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        IQueryable<Schedule> GetIQueryableWithDoctor();
    }
}

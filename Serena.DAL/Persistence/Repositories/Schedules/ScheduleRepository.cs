using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Schedules
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public IQueryable<Schedule> GetIQueryableWithDoctor()
        {
            return _dbContext.Set<Schedule>()
                             .Include(s => s.Doctor)
                             .Where(s => !s.IsDeleted);
        }
    }
}

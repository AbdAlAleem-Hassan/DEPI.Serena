using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.DoctorLanguages;
using Serena.DAL.Persistence.Repositories.Doctors;
using Serena.DAL.Persistence.Repositories.DoctorServices;
using Serena.DAL.Persistence.Repositories.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.UnitOfWork
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		public IDoctorRepository DoctorRepository { get; }
		public IServiceRepository ServiceRepository { get;  }
		public ILanguageRepository LanguageRepository { get;  }
		public IDoctorLanguageRepository DoctorLanguageRepository { get;  }
		public IDepartmentRepository DepartmentRepository { get; }

        Task<int> CompleteAsync();
        
    }
}

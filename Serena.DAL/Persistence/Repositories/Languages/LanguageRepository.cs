using Serena.DAL.Entities;
using Serena.DAL.Persistence.Data;
using Serena.DAL.Persistence.Repositories._GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Languages
{
	public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
	{
		public LanguageRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}

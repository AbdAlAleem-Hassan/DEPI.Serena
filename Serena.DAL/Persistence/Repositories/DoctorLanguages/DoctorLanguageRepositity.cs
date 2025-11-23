using Serena.DAL.Entities;
using Serena.DAL.Persistence.Data;
using Serena.DAL.Persistence.Repositories._GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.DoctorLanguages
{
	public class DoctorLanguageRepositity : GenericRepository<DoctorLangauge>, IDoctorLanguageRepository
	{
		public DoctorLanguageRepositity(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	{
	}
}

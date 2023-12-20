using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.DataService.Repositories.Database
{
    public abstract class BaseDatabaseRepository
    {
        protected readonly RegionDbContext _context;

        protected BaseDatabaseRepository(RegionDbContext context)
        {
            _context = context;
        }
    }
}

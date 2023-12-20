using Microsoft.EntityFrameworkCore;
using Prj_final_METEO.DataService.Repositories.Interfaces;
using Prj_final_METEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.DataService.Repositories.Database
{
    public class RegionDatabaseRepository : BaseDatabaseRepository, IRegionRepository
    {
        public RegionDatabaseRepository(RegionDbContext context) : base(context) { }

        public List<Region> GetAll()
        {
            return _context.Region.OrderBy(r => r.Id).ToList();
        }

        public void Add(Region region)
        {
            _context.Region.Add(region);
            _context.SaveChanges();
        }

        public void Del(Region region)
        {
            _context.Remove(region);
            _context.SaveChanges();
        }
    }
}

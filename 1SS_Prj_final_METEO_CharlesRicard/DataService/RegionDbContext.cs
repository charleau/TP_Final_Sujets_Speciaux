using Microsoft.EntityFrameworkCore;
using Prj_final_METEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.DataService
{
    public class RegionDbContext : DbContext
    {
        public RegionDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Region> regions = new List<Region>()
            {
                new Region(1, "Shawinigan", 46.56984172477484, -72.73811285651442)
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }

        public DbSet<Region> Region { get; set; }
    }
}

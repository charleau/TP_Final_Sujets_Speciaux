using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.DataService
{
    public class SqliteRegionDbContext : RegionDbContext
    {
        public SqliteRegionDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Properties.Settings.Default.DataBaseConnexionChaine);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Prj_final_METEO.DataService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Prj_final_METEO
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string lang = Prj_final_METEO.Properties.Settings.Default.langue;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //AutofacConfig();
            DbMigrate();
        }

        private void DbMigrate()
        {
            SqliteRegionDbContext context = new SqliteRegionDbContext();
            context.Database.Migrate();
        }
    }
}

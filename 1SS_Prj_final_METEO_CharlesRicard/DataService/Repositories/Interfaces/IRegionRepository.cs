using Prj_final_METEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.DataService.Repositories.Interfaces
{
    public interface IRegionRepository 
    {
        List<Region> GetAll();
    }
}

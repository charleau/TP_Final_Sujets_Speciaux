using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.Models
{
    public class Result
    {
        public DateOnly Date { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public string IconCode { get; set; }

        public Result(DateOnly Date, double MinTemp, double MaxTemp, string IconCode)
        {
            this.Date = Date;
            this.MinTemp = MinTemp;
            this.MaxTemp = MaxTemp;
            this.IconCode = IconCode;
        }
    }
}

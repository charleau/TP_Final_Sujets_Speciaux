using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj_final_METEO.Models
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public Region(int Id, string Name, double Latitude, double Longitude)
        {
            this.Id = Id;
            this.Name = Name;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }
    }
}

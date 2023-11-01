using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Models
{
    [Table("Flight")]
    public class FlightModel
    {
        [Key]
        public string FlightId { get; set; }
        public DateTime Date { get; set; }
        public string PointOfLoading { get; set; } 
        public string PointOfUnloading { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoAnhVu_DuAn2.Models
{
    public class FlightModel
    {
        public string FlightId { get; set; }
        public DateTime Date { get; set; }
        public string PointOfLoading { get; set; }
        public string PointOfUnloading { get; set; }
    }
}

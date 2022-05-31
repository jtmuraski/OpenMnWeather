using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenMnWeather.Models
{
    public class ShortViewMetar
    {
        public int Id { get; set; }
        public DateTime ObservationTime {get; set;}
        public double TempC { get; set; }
        public double Precp { get; set; }
        public string RawText { get; set; }

    }
}

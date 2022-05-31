using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenMnWeather.Models
{
    public class QuickViewTreeGridModel
    {
        public string StationId { get; set; }
        public List<ShortViewMetar> ShortMetars { get; set; } = new List<ShortViewMetar>();
    }
}

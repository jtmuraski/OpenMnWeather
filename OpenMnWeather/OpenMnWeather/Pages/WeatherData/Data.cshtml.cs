using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MetarAPI.Services;
using MetarAPI.Models.DataModels;

namespace OpenMnWeather.Pages.WeatherData
{
    public class DataModel : PageModel
    {
        // DI models
        public IConfiguration _config;
        private readonly IMetarActions _metars;

        // Page Models
        public IEnumerable<Metar> Metars { get; set; }

        public DataModel(IConfiguration config, IMetarActions metars)
        {
            this._config = config;
            this._metars = metars;
        }
        public IActionResult OnGet()
        {
            Metars = _metars.FilterByTime(DateTime.Now.AddHours(-48), DateTime.Now);
            Metars = Metars.OrderBy(report => report.ObservationTime);
            return Page();
        }
    }
}

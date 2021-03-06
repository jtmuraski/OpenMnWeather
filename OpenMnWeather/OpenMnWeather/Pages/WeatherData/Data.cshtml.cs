using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MetarAPI.Services;
using MetarAPI.Models.DataModels;
using Syncfusion.EJ2;
using OpenMnWeather.Models;
using System.ComponentModel.DataAnnotations;

namespace OpenMnWeather.Pages.WeatherData
{
    public class DataModel : PageModel
    {
        // DI models
        public IConfiguration _config;
        private readonly IMetarActions _metars;

        // Page Models
        public IEnumerable<Metar> Metars;
        //public DateRangePicker DateRange = new DateRangePicker() { Dates = new DateTime?[] { DateTime.Now.AddDays(-2), DateTime.Now } };
        [BindProperty(SupportsGet =true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet =true)]
        public DateTime? EndDate { get; set; }
        public DataModel(IConfiguration config, IMetarActions metars)
        {
            this._config = config;
            this._metars = metars;
        }
        public IActionResult OnGet(DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null && (DateTime)startDate != DateTime.MinValue)
            {
                Metars = _metars.FilterByTime((DateTime)startDate, (DateTime)endDate);
                Metars = Metars.OrderBy(report => report.ObservationTime);
            }
            else
            {
                StartDate = DateTime.Now.AddDays(-2);
                EndDate = DateTime.Now;
                Metars = _metars.FilterByTime((DateTime)StartDate, (DateTime)EndDate);
                Metars = Metars.OrderBy(report => report.ObservationTime);
                //DateRange = new DateRangePicker() { Dates = new DateTime?[] { DateTime.Now.AddDays(-2), DateTime.Now } };
            }                        
            return Page();
        }

        public IActionResult OnPost()
        {
            if (StartDate == DateTime.MinValue)
                return RedirectToPage("./Error");
            return RedirectToPage("./Data", new { start = StartDate, end = EndDate });
            //return RedirectToPage("./Data", new { start = StartDate, end = EndDate });
        }
    }
}

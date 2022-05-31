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
        public IEnumerable<IGrouping<string, Metar>> GroupedMetars;
        [BindProperty(SupportsGet =true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet =true)]
        public DateTime? EndDate { get; set; }
        public List<QuickViewTreeGridModel> GridModels { get; set; }


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
                GroupedMetars = Metars.GroupBy(metar => metar.StationId);
            }
            else
            {
                StartDate = DateTime.Now.AddDays(-2);
                EndDate = DateTime.Now;
                Metars = _metars.FilterByTime((DateTime)StartDate, (DateTime)EndDate);
                Metars = Metars.OrderBy(report => report.StationId);
                //DateRange = new DateRangePicker() { Dates = new DateTime?[] { DateTime.Now.AddDays(-2), DateTime.Now } };
            }

            var stations = Metars.Select(station => station.StationId).Distinct();
            GridModels = new List<QuickViewTreeGridModel>();
            foreach(var station in stations)
            {
                QuickViewTreeGridModel stationModel = new QuickViewTreeGridModel();
                stationModel.StationId = station;
               
                var stationMetars = Metars.Where(metar => metar.StationId == station).ToList();
                foreach(var metar in stationMetars)
                {
                    ShortViewMetar shortViewMetar = new ShortViewMetar()
                    {
                        Id = metar.Id,
                        ObservationTime = metar.ObservationTime,
                        TempC = metar.TempC,
                        Precp = metar.PrecipIn,
                        RawText = metar.RawText
                    };
                    stationModel.ShortMetars.Add(shortViewMetar);
                }
                GridModels.Add(stationModel);
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

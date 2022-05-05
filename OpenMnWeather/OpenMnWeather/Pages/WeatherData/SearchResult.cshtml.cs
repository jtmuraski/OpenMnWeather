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
    public class SearchResultModel : PageModel
    {
        // DI models
        public IConfiguration _config;
        private readonly IMetarActions _metars;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public SearchResultModel(IConfiguration config, IMetarActions metars)
        {
            this._config = config;
            this._metars = metars;
        }
        public IActionResult OnGet(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
            return Page();
        }
    }
}

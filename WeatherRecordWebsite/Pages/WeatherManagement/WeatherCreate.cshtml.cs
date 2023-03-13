using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WeatherRecordWebsite.Data;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.WeatherManagement
{
    [Authorize(Roles = "Admin")]
    public class WeatherCreateModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public WeatherCreateModel(WeatherContext context)
        {
            _context = context;
        }

        [BindProperty]
        public City City { get; set; }
        [BindProperty]
        public Record Record { get; set; }
        [BindProperty]
        public Temperature Temperature { get; set; }
        [BindProperty]
        public WindSpeed WindSpeed { get; set; }
        [BindProperty]
        public Weather Weather { get; set; }

        public IActionResult OnGet(int? id)
        {
            City = _context.Cities.FirstOrDefault(c => c.Id == id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Temperature.Add(Temperature);
            _context.SaveChanges();
            _context.Entry(Temperature);
            Record.TemperatureId = Temperature.Id;


            _context.WindSpeed.Add(WindSpeed);
            _context.SaveChanges();
            _context.Entry(WindSpeed);
            Record.WindSpeedId = WindSpeed.Id;

            _context.Weather.Add(Weather);
            _context.SaveChanges();
            _context.Entry(Weather);
            Record.WeatherId = Weather.Id;

            Record.CityId = City.Id;
            _context.Records.Add(Record);
            _context.SaveChanges();

            return RedirectToPage("./Details", new { id = City.Id });
        }
    }
}

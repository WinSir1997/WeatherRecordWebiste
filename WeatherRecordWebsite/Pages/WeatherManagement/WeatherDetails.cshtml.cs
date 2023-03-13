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
    public class WeatherDetailsModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public WeatherDetailsModel(WeatherContext context)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            Record = await _context.Records.FirstOrDefaultAsync(c => c.Id == id);
            if (Record == null)
            {
                return NotFound();
            }
            else
            {
                City = await _context.Cities.FindAsync(Record.CityId);
                Temperature = await _context.Temperature.FindAsync(Record.TemperatureId);
                WindSpeed = await _context.WindSpeed.FindAsync(Record.WindSpeedId);
                Weather = await _context.Weather.FindAsync(Record.WeatherId);

            }

            return Page();
        }
    }
}

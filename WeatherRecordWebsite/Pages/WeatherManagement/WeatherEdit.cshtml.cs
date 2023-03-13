using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.WeatherManagement
{
    [Authorize(Roles = "Admin")]
    public class WeatherEditModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public WeatherEditModel(WeatherRecordWebsite.Data.WeatherContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Temperature).State = EntityState.Modified;
            _context.Attach(WindSpeed).State = EntityState.Modified;
            _context.Attach(Weather).State = EntityState.Modified;
            _context.Attach(Record).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperatureExists(Temperature.Id) || !WindSpeedExists(WindSpeed.Id) || !WeatherExists(Weather.Id)
                    || !RecordExists(Record.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = City.Id });
        }
        private bool TemperatureExists(int id)
        {
            return _context.Temperature.Any(e => e.Id == id);
        }
        private bool WindSpeedExists(int id)
        {
            return _context.WindSpeed.Any(e => e.Id == id);
        }
        private bool WeatherExists(int id)
        {
            return _context.Weather.Any(e => e.Id == id);
        }
        private bool RecordExists(int id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}

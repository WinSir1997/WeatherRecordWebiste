using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherRecordWebsite.Data;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.View
{
    public class IndexModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public IndexModel(WeatherContext context)
        {
            _context = context;
        }

        public City City { get; set; }
        public IList<Record> Record { get; set; }
        public IList<RecordView> RecordViews { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                City = city;
                if (_context.Records == null)
                {
                    return Page();
                }

                var records = _context.Records.Where(r => r.CityId == id).OrderBy(o => o.Date).ToList();
                if (records == null)
                {
                    return Page();
                }

                RecordViews = new List<RecordView>();
                foreach (var record in records)
                {
                    var temperature = await _context.Temperature.FirstOrDefaultAsync(t => t.Id == record.TemperatureId);
                    var windSpeed = await _context.WindSpeed.FirstOrDefaultAsync(w => w.Id == record.WindSpeedId);
                    var weather = await _context.Weather.FirstOrDefaultAsync(w => w.Id == record.WeatherId);

                    RecordView recordView = new()
                    {
                        Id = record.Id,
                        Date = record.Date.ToShortDateString(),
                        Temperature = temperature,
                        WindSpeed = windSpeed,
                        Weather = weather,
                        City = City
                    };

                    RecordViews.Add(recordView);
                }

                return Page();
            }
        }
    }
}

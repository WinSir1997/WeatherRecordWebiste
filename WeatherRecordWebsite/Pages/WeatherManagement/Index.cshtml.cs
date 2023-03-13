using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.WeatherManagement
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public IndexModel(WeatherRecordWebsite.Data.WeatherContext context)
        {
            _context = context;
        }

        public IList<City> Citys { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.Cities != null)
            {
                Citys = _context.Cities.ToList();
            }
            return Page();
        }
    }
}

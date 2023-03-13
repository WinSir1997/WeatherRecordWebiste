using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;
        public IndexModel(ILogger<IndexModel> logger, Data.WeatherContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public IList<City> Cities { get; set; }

        public IActionResult OnGet()
        {
            Cities = _context.Cities.ToList();
            return Page();
        }
    }
}
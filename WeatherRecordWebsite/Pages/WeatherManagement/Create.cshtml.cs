using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using WeatherRecordWebsite.Data;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.WeatherManagement
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public CreateModel(WeatherRecordWebsite.Data.WeatherContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public City City { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cities.Add(City);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

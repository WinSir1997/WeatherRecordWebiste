using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.UserManagement
{
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
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

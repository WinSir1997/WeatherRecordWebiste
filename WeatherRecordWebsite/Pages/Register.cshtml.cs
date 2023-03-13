using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public RegisterModel(WeatherRecordWebsite.Data.WeatherContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public SelectList TypeList { get; set; }

        public IActionResult OnGet()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem{Value="1",Text="Admin"},
                new SelectListItem{Value="2",Text="Consumer"}
            };

            TypeList = new SelectList(items,"Value","Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Login");
        }
    }
}

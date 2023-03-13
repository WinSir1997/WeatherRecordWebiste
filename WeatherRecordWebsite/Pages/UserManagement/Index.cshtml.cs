using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherRecordWebsite.Models;

namespace WeatherRecordWebsite.Pages.UserManagement
{
    public class IndexModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public IndexModel(WeatherRecordWebsite.Data.WeatherContext context)
        {
            _context = context;
        }

        public IList<User> Users { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if(_context.Users != null)
            {
                Users = await _context.Users.ToListAsync();
            }
        }
    }
}

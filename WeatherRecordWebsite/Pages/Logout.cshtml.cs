using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeatherRecordWebsite.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            //HttpContext.Session.Remove("state");
            //HttpContext.Session.Remove("uname");
            //HttpContext.SignOutAsync();

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("./Index");
        }
    }
}

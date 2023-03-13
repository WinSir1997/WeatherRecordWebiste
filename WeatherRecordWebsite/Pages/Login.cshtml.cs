using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WeatherRecordWebsite.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.Razor;

namespace WeatherRecordWebsite.Pages
{
    public class LoginModel : PageModel
    {
        private readonly WeatherRecordWebsite.Data.WeatherContext _context;

        public LoginModel(WeatherRecordWebsite.Data.WeatherContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public IActionResult OnGet()
        {
           return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           
            var user = _context.Users.Where(item => item.Id == User.Id).FirstOrDefault();
            if (user == null)
            {
                return RedirectToPage("./Register");
            }
            else
            {
                var claims = new List<Claim>() //用Claim保存用户信息
                {
                    new Claim(ClaimTypes.Name,"账号"),
                    new Claim("id",user.Id),
                    new Claim("account",user.Name),
                };

                string url = "./WeatherManagement/Index";
                if (user.type.Equals("1"))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else if (user.type.Equals("2"))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Consumer"));
                    url = "./Index";
                }

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(30)
                    }).Wait();

                return RedirectToPage(url);
            }
        }
    }
}

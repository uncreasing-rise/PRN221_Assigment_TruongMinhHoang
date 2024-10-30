using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Security.Claims;

namespace BookStore_TruongMinhHoang.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly IAccountRepo _accountRepo;

        public IndexModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public void OnGet()
        {
            // Any initialization logic for GET requests can go here.
        }

        public async Task<IActionResult> OnPost()
        {
            var account = await _accountRepo.Login(Username, Password);
            if (account != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Username),
            new Claim(ClaimTypes.Role, account.Role) // Giả sử account.Role là "Admin"
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Giữ người dùng đăng nhập
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Admin/BookManagementPage/Index"); // Redirect đến trang quản lý
            }
            else
            {
                TempData["Message"] = "Invalid username or password.";
                return Page();
            }
        }
        public async Task<IActionResult> OnPostLogout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Sign out the user
            await HttpContext.SignOutAsync();

            // Redirect to the home page or login page after logout
            return RedirectToPage("/Index");
        }
    }
}

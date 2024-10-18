using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public string password { get; set; }

        private readonly IAccountRepo _accountRepo;

        public IndexModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var account = await _accountRepo.Login(username, password);
                if (account != null)
                {
                    TempData["Message"] = "Login Success";
                    Console.WriteLine("Login Success");

                    //set session
                    HttpContext.Session.SetString("Username", username);
                    //HttpContext.Session.SetInt32("RoleId", account.Role ?? default(int));

                    return RedirectToPage("/OilPaintingArtPage/Index");
                }
                else
                {
                    TempData["Message"] = "You do not have permission to do this function";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }
        }

    }
}

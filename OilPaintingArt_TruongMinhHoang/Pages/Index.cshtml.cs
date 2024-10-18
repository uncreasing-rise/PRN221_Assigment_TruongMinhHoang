using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        private readonly IAccountRepo _accountRepo;  
        public IndexModel(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task OnPost()
        {
            try
            {
                var account = await _accountRepo.Login(email, password);
                if (account != null)
                {
                    TempData["Message"] = "Login successfully";
                    Response.Redirect("/OilPaintingArtPage");
                }
                else
                {

                    TempData["Message"] = "You do not have permission to do this function!";
                    Response.Redirect("/");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

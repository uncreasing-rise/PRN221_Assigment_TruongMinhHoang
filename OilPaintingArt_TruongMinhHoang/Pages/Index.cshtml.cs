using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace OilPaintingArt_TruongMinhHoang.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        private readonly ISystemAccountRepo systemAccountRepo;  
        public IndexModel(ISystemAccountRepo systemAccountRepo)
        {
            this.systemAccountRepo = systemAccountRepo;
        }
        public async Task OnPost()  
        {
            try
            {
                var account = await systemAccountRepo.Login(email, password);
                if(account != null && (account.Role == 2 || account.Role == 3))
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

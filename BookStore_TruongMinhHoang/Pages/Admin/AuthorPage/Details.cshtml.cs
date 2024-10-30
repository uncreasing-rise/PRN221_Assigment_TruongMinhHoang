using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.AuthorPage
{
    public class DetailsModel : PageModel
    {
        private readonly IAuthorRepo _authorRepo;

        public DetailsModel(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public Author Author { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author = await _authorRepo.GetAuthorById(id.Value);
            if (Author == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

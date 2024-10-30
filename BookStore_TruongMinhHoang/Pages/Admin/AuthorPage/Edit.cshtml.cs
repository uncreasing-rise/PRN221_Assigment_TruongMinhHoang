using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.AuthorPage
{
    public class EditModel : PageModel
    {
        private readonly IAuthorRepo _authorRepo; // Use IAuthorRepo for abstraction

        public EditModel(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync()
        {
         
                await _authorRepo.UpdateAuthor(Author);
            
           

            return RedirectToPage("./Index");
        }

    }
}

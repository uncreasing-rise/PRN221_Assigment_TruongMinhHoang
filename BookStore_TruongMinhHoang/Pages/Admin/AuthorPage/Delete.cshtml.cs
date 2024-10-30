using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.AuthorPage
{
    public class DeleteModel : PageModel
    {
        private readonly IAuthorRepo _authorRepo;

        public DeleteModel(IAuthorRepo authorRepo)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the author to be deleted
            var authorToDelete = await _authorRepo.GetAuthorById(id.Value);
            if (authorToDelete == null)
            {
                return NotFound();
            }

            // Remove the author using the repository
            await _authorRepo.DeleteAuthor(authorToDelete.AuthorId);

            // Redirect to the index page after deletion
            return RedirectToPage("./Index");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.BookManagementPage
{
    public class DeleteModel : PageModel
    {
        private readonly IBookRepo _bookRepo;

        public DeleteModel(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use the repository to fetch the book
            Book = await _bookRepo.GetBookById(id.Value);

            if (Book == null)
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

            // Use the repository to find the book to delete
            var bookToDelete = await _bookRepo.GetBookById(id.Value);
            if (bookToDelete != null)
            {
                await _bookRepo.DeleteBook(bookToDelete.BookId); 
            }

            return RedirectToPage("./Index");
        }
    }
}

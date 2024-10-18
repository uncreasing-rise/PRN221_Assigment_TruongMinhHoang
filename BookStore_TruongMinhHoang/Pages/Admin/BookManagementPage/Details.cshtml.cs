using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.BookManagementPage
{
    public class DetailsModel : PageModel
    {
        private readonly IBookRepo _bookRepo;

        public DetailsModel(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the book along with related Author and Publisher
            Book = await _bookRepo.GetBookById(id.Value);
            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

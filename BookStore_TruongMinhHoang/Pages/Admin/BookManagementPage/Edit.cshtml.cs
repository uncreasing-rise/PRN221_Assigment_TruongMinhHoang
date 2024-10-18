using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.BookManagementPage
{
    public class EditModel : PageModel
    {
        private readonly IBookRepo _bookRepo;
        private readonly IPushlisherRepo _pushlisherRepo;
        private readonly IAuthorRepo _authorRepo;

        public EditModel(IBookRepo bookRepo, IAuthorRepo authorRepo, IPushlisherRepo pushlisherRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _pushlisherRepo = pushlisherRepo;   
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use the repository to get the book
            Book = await _bookRepo.GetBookById(id.Value);
            if (Book == null)
            {
                return NotFound();
            }

            // Populate ViewData for the dropdowns
            await PopulateAuthorsAndPublishersAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateAuthorsAndPublishersAsync();
                return Page();
            }

            // Update the book using the repository
            await _bookRepo.UpdateBook(Book);

            return RedirectToPage("./Index");
        }

        private async Task PopulateAuthorsAndPublishersAsync()
        {
            // Retrieve authors and publishers
            var authors = await _authorRepo.GetAuthors();
            var publishers = await _pushlisherRepo.GetPublishers();

            // Populate ViewData with authors
            ViewData["AuthorId"] = new SelectList(authors, "AuthorId", "LastName");

            // Populate ViewData with publishers
            ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "PublisherName");
        }

    }
}

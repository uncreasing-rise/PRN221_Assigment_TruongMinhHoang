using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.BookManagementPage
{
    public class CreateModel : PageModel
    {
        private readonly IBookRepo _bookRepo;
        private readonly IAuthorRepo _authorRepo;
        private readonly IPublisherRepo _publisherRepo;

        public CreateModel(IBookRepo bookRepo, IAuthorRepo authorRepo, IPublisherRepo publisherRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _publisherRepo = publisherRepo;
        }

        public IActionResult OnGet()
        {
            // Populate ViewData with authors and publishers using repository methods
            PopulateAuthorsAndPublishers();
            return Page();
        }

        private async Task PopulateAuthorsAndPublishers()
        {
            var authors = await _authorRepo.GetAuthors();
            var publishers = await _publisherRepo.GetPublishers();

            ViewData["AuthorId"] = new SelectList(authors, "AuthorId", "LastName");
            ViewData["PublisherId"] = new SelectList(publishers, "PublisherId", "PublisherName");
        }


        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingBook = await _bookRepo.GetBookById(Book.BookId); // Assuming you have a method to check the ISBN
            if (existingBook != null)
            {
                ModelState.AddModelError("Book.Isbn", "A book with this ISBN already exists.");
                return Page();
            }

            await _bookRepo.AddBook(Book); // Ensure this is asynchronous
            return RedirectToPage("./Index");
        }


    }
}

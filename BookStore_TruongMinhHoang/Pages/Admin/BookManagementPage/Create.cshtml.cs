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
            // Populate ViewData with authors and publishers
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

            await _bookRepo.AddBook(Book); // Assuming you have an AddBookAsync method in IBookRepo

            return RedirectToPage("./Index");
        }
    }
}

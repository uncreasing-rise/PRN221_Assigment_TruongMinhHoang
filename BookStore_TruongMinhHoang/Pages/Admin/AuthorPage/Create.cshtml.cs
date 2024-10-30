using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;

namespace BookStore_TruongMinhHoang.Pages.Admin.AuthorPage
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorRepo _authorRepo;

        public CreateModel(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _authorRepo.AddAuthor(Author);
            return RedirectToPage("./Index");
        }
    }
}

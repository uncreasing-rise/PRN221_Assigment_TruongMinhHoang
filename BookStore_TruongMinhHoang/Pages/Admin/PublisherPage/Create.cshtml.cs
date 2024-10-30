using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories; // Ensure this is the correct namespace for your repository

namespace BookStore_TruongMinhHoang.Pages.Admin.PublisherPage
{
    public class CreateModel : PageModel
    {
        private readonly IPublisherRepo _publisherRepository;

        public CreateModel(IPublisherRepo publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _publisherRepository.AddPublisher(Publisher); // Use the repository to add the publisher

            return RedirectToPage("./Index");
        }
    }
}

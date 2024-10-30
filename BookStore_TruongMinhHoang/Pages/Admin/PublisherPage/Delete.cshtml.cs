using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories; // Ensure this is the correct namespace for your repository

namespace BookStore_TruongMinhHoang.Pages.Admin.PublisherPage
{
    public class DeleteModel : PageModel
    {
        private readonly IPublisherRepo _publisherRepository;

        public DeleteModel(IPublisherRepo publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use the repository to get the publisher
            Publisher = await _publisherRepository.GetPublisherById(id.Value);

            if (Publisher == null)
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

            // Use the repository to find and delete the publisher
            var publisher = await _publisherRepository.GetPublisherById(id.Value);
            if (publisher != null)
            {
                await _publisherRepository.DeletePublisher(id.Value); // Ensure this method exists in your repository
            }

            return RedirectToPage("./Index");
        }
    }
}

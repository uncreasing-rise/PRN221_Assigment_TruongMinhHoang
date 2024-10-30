using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories; // Ensure this is the correct namespace for your repository

namespace BookStore_TruongMinhHoang.Pages.Admin.PublisherPage
{
    public class DetailsModel : PageModel
    {
        private readonly IPublisherRepo _publisherRepository;

        public DetailsModel(IPublisherRepo publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

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
    }
}

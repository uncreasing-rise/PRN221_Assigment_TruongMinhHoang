using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore_TruongMinhHoang.Pages.Admin.PublisherPage
{
    public class EditModel : PageModel
    {
        private readonly IPublisherRepo _publisherRepository;

        public EditModel(IPublisherRepo publisherRepository)
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

            Publisher = await _publisherRepository.GetPublisherById(id.Value);
            if (Publisher == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        public async Task<IActionResult> OnPostAsync()
        {
      
                await _publisherRepository.UpdatePublisher(Publisher);
            
           

            return RedirectToPage("./Index");
        }

    }
}

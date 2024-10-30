using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BookStore_TruongMinhHoang.Pages.Admin.PublisherPage
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin"


    public class IndexModel : PageModel
    {
        private readonly IPublisherRepo _publisherRepository;

        public IndexModel(IPublisherRepo publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public IList<Publisher> Publisher { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Publisher = await _publisherRepository.GetPublishers();
        }

    }
}

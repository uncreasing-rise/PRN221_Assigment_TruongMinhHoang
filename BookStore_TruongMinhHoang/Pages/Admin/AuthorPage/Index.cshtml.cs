using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookStore_TruongMinhHoang.Pages.Admin.AuthorPage
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin"

    public class IndexModel : PageModel
    {
        private readonly IAuthorRepo _authorRepo;

        public IndexModel(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public IList<Author> Authors { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Authors = await _authorRepo.GetAuthors();
        }
    }
}

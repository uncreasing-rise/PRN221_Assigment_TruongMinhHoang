using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookStore_TruongMinhHoang.Pages.Admin.BookManagementPage
{
    [Authorize(Roles = "Admin")] // Chỉ cho phép người dùng có vai trò "Admin"

    public class IndexModel : PageModel
    {


        //search
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        //paging
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 2;

        private readonly IBookRepo _bookRepo;

        public IndexModel(IBookRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync(int pageIndex = 1)
        {
            var result = await _bookRepo.GetList(searchTerm, pageIndex, 2);

            Book = result.Books;
            PageIndex = result.PageIndex;
            TotalPages = result.TotalPages;
        }

    }
}

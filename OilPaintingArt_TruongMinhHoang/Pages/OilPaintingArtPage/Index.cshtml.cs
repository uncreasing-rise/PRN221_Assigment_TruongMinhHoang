using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;
using Repositories;

namespace OilPaintingArt_TruongMinhHoang.Pages.OilPaintingArtPage
{
    public class IndexModel : PageModel
    {
        private readonly IOilPaintingRepo _context;
        public IndexModel(IOilPaintingRepo context)
        {
            _context = context;
        }

        public IList<OilPaintingArt> OilPaintingArt { get;set; } = default!;

        public async Task OnGetAsync()
        {
            OilPaintingArt = await _context.GetList();
        }
    }
}
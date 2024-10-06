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
    public class DetailsModel : PageModel
    {
        private readonly IOilPaintingRepo _context;
        public DetailsModel(IOilPaintingRepo context)
        {
            _context = context;
        }

        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            OilPaintingArt = await _context.GetOilPaintingArtById(id ?? default(int));
            return Page();
        }
    }
}

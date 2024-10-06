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
    public class DeleteModel : PageModel
    {
        private readonly IOilPaintingRepo _context;
        public DeleteModel(IOilPaintingRepo context)
        {
            _context = context;
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oilpaintingart = await _context.GetOilPaintingArtById(id ?? default(int));

            if (oilpaintingart == null)
            {
                return NotFound();
            }
            else
            {
                OilPaintingArt = oilpaintingart;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _context.DeleteOilPaintingArtById(id ?? default(int));   
            TempData["Message"] = "Oil Painting Art Deleted Successfully!";
            return RedirectToPage("./Index");
        }
    }
}

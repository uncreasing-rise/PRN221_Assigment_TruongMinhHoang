using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;
using Repositories;

namespace OilPaintingArt_TruongMinhHoang.Pages.OilPaintingArtPage
{

    public class EditModel : PageModel
    {
        private readonly IOilPaintingRepo _artRepo;
        private readonly ISupplierCompanyRepo _supplierRepo;

        public EditModel(IOilPaintingRepo artRepo, ISupplierCompanyRepo supplierRepo)
        {
            _artRepo = artRepo;
            _supplierRepo = supplierRepo;
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oilpaintingart = await _artRepo.GetOilPaintingArtById(id ?? default(int));
            if (oilpaintingart == null)
            {
                return NotFound();
            }
            OilPaintingArt = oilpaintingart;

            var list = await _supplierRepo.GetSuppliers();

            ViewData["SupplierId"] = new SelectList(list, "SupplierId", "CompanyName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                await _artRepo.UpdateOilPaintingArt(OilPaintingArt);
                TempData["Message"] = "Update Succesfull";

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return Page();
            }
        }
    }
}

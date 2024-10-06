using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DAOs;
using Repositories;

namespace OilPaintingArt_TruongMinhHoang.Pages.OilPaintingArtPage
{

    public class CreateModel : PageModel
    {
        private readonly IOilPaintingRepo _artRepo;
        private readonly ISupplierCompanyRepo _supplierRepo;

        public CreateModel(IOilPaintingRepo artRepo, ISupplierCompanyRepo supplierRepo)
        {
            _artRepo = artRepo;
            _supplierRepo = supplierRepo;
        }

        public async Task<IActionResult> OnGet()
        {
            var listItem = await _supplierRepo.GetSuppliers();
            ViewData["SupplierId"] = new SelectList(listItem, "SupplierId", "CompanyName");
            return Page();
        }

        [BindProperty]
        public OilPaintingArt OilPaintingArt { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _artRepo.AddOilPaintingArt(OilPaintingArt);
                TempData["Message"] = "Create Succesfull";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return await OnGet();
            }
        }
    }
}

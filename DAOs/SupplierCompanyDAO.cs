using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class SupplierCompanyDAO

    {
        private readonly OilPaintingArt2024DbContext _context;
        private static SupplierCompanyDAO instance = null;
        private SupplierCompanyDAO()
        {
            _context = new OilPaintingArt2024DbContext();
        }

        public static SupplierCompanyDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new SupplierCompanyDAO();
                }
                return instance;
            }
        }

        public async Task<List<SupplierCompany>> GetSuppliers()
        {
            return await _context.SupplierCompanies.ToListAsync();
        }
    }
}

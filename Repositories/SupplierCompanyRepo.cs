using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SupplierCompanyRepo : ISupplierCompanyRepo
    {
        public async Task<List<SupplierCompany>> GetSuppliers()
        {
            return await SupplierCompanyDAO.Instance.GetSuppliers();
        }
    }
}

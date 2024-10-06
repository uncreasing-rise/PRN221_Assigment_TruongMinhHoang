using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class SystemAccountDAO
    {
        private readonly OilPaintingArt2024DbContext _context;
        private static SystemAccountDAO instance = null;
        private SystemAccountDAO()
        {
            _context = new OilPaintingArt2024DbContext();
        }

        public static SystemAccountDAO Instance
        {
            get
            {
                if(instance == null)
                {
                    return new SystemAccountDAO();
                }
                return instance;
            }
        }

        public async Task<SystemAccount> GetSystemAccount(string email, string password)
        {
            return await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email && x.AccountPassword == password); 
        }



    }
}

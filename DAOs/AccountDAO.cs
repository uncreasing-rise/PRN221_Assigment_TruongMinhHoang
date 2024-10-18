using BusinessObjects;
using Daos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class AccountDAO
    {

        private readonly BookStoreContext _context;
        private static AccountDAO instance = null;

        private AccountDAO()
        {
            _context = new BookStoreContext();
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new AccountDAO();
                }
                return instance;
            }
        }

        public async Task<Account> Login(string email, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(acc => acc.Username == email && acc.PasswordHash == password);
        }

    }
}

using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepo : IAccountRepo
    {
        public async Task<Account> Login(string username, string password)
        {
            return await AccountDAO.Instance.Login(username, password);
        }
    }
}

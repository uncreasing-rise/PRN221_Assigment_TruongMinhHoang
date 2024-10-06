using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories
{
    public class SystemAccountRepo : ISystemAccountRepo
    {
        public Task<SystemAccount> Login(string email, string password)
        {
            return SystemAccountDAO.Instance.GetSystemAccount(email, password);
        }
    }
}

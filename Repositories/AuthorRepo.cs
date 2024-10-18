using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        public Task<List<Author>> GetAuthors()
        {
            return AuthorDAO.Instance.GetAuthors();        }
    }
}

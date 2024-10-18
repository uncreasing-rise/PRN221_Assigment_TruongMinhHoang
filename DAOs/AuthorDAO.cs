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
    public class AuthorDAO
    {
        private readonly BookStoreContext _context;
        private static AuthorDAO instance = null;

        private AuthorDAO()
        {
            _context = new BookStoreContext();
        }

        public static AuthorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new AuthorDAO();
                }
                return instance;
            }
        }
        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

    }
}

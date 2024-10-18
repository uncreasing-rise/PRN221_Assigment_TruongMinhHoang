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
    public class PushlisherDAO
    {
        private readonly BookStoreContext _context;
        private static PushlisherDAO instance = null;

        private PushlisherDAO()
        {
            _context = new BookStoreContext();
        }

        public static PushlisherDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    return new PushlisherDAO();
                }
                return instance;
            }
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

    }
}

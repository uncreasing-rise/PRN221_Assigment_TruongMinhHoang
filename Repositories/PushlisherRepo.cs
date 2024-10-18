using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PushlisherRepo : IPushlisherRepo
    {
        public Task<List<Publisher>> GetPublishers()
        {
        return PushlisherDAO.Instance.GetPublishers();
        }
    }
}

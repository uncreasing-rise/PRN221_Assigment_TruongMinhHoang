using BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPublisherRepo
    {
        Task<List<Publisher>> GetPublishers();
        Task<Publisher> GetPublisherById(int id);
        Task AddPublisher(Publisher publisher);
        Task UpdatePublisher(Publisher publisher);
        Task DeletePublisher(int id);
    }
}

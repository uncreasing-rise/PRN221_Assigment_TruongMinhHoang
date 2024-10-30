using BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using Daos;

namespace Repositories
{
    public class PublisherRepo : IPublisherRepo
    {

      
        public async Task<List<Publisher>> GetPublishers()
        {
            return await PublisherDAO.Instance.GetPublishers();
        }

        public async Task<Publisher> GetPublisherById(int id)
        {
            return await PublisherDAO.Instance.GetPublisherById(id);
        }

        public async Task AddPublisher(Publisher publisher)
        {
            await PublisherDAO.Instance.AddPublisher(publisher);
        }

        public async Task UpdatePublisher(Publisher publisher)
        {
            await PublisherDAO.Instance.UpdatePublisher(publisher);
        }

        public async Task DeletePublisher(int id)
        {
            await PublisherDAO.Instance.DeletePublisher(id);
        }
    }
}

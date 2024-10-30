using BusinessObjects;
using BusinessObjects.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{   
    public interface IAuthorRepo { 
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);
        Task AddAuthor(Author author);
        Task UpdateAuthor(Author author);
        Task DeleteAuthor(int id);
    }
}

using BusinessObjects;
using BusinessObjects.Response;
using Daos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthorRepo : IAuthorRepo
    {
        public async Task<List<Author>> GetAuthors()
        {
            return await AuthorDAO.Instance.GetAuthors();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await AuthorDAO.Instance.GetAuthorById(id);
        }

        public async Task AddAuthor(Author author)
        {
            await AuthorDAO.Instance.AddAuthor(author);
        }

        public async Task UpdateAuthor(Author author)
        {
            await AuthorDAO.Instance.UpdateAuthor(author);
        }

        public async Task DeleteAuthor(int id)
        {
            await AuthorDAO.Instance.DeleteAuthor(id);
        }

        public async Task<AuthorResponse> GetPaginatedAuthors(string searchTerm, int pageIndex, int pageSize)
        {
            return await AuthorDAO.Instance.GetPaginatedAuthors(searchTerm, pageIndex, pageSize);
        }
    }
}

using BusinessObjects;
using BusinessObjects.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookRepo
    {
        // Get all books
        Task<List<Book>> GetList();

        // Get a book by its ID
        Task<Book> GetBookById(int id);

        // Get paginated list of books with search functionality
        Task<BookRespone> GetList(string searchTerm, int pageIndex, int pageSize);

        // Add a new book
        Task AddBook(Book book);

        // Update an existing book
        Task UpdateBook(Book book);

        // Delete a book by its ID
        Task DeleteBook(int id);
    }

    // Response class for paginated book queries

}

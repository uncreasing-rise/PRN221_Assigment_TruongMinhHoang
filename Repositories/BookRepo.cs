using BusinessObjects;
using BusinessObjects.Response;
using DAOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookRepo : IBookRepo
    {


        // Get all books
        public async Task<List<Book>> GetList()
        {
            return await BookDAO.Instance.GetBooks(); 
        }

        // Get a book by its ID
        public async Task<Book> GetBookById(int id)
        {
            return await BookDAO.Instance.GetBookById(id);
        }

        // Get a paginated list of books with search functionality
        public async Task<BookRespone> GetList(string searchTerm, int pageIndex, int pageSize)
        {
            return await BookDAO.Instance.GetPaginatedBooks(searchTerm, pageIndex, pageSize);
        }

        // Add a new book
        public async Task AddBook(Book book)
        {
            await BookDAO.Instance.AddBook(book);
        }

        // Update an existing book
        public async Task UpdateBook(Book book)
        {
            await BookDAO.Instance.UpdateBook(book);
        }

        // Delete a book by its ID
        public async Task DeleteBook(int id)
        {
            await BookDAO.Instance.DeleteBook(id);
        }

     
    }
}

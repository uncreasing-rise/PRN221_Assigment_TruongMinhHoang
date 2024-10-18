using BusinessObjects;
using BusinessObjects.Response;
using Daos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAOs
{
    public class BookDAO
    {
        private readonly BookStoreContext _context;
        private static BookDAO instance = null;

        private BookDAO()
        {
            _context = new BookStoreContext();
        }

        public static BookDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookDAO();
                }
                return instance;
            }
        }

        // Get all books
        public async Task<List<Book>> GetBooks()
        {
            return await _context.Books.Include(b => b.Author) // Include Author if needed
                                        .Include(b => b.Publisher) // Include Publisher if needed
                                        .ToListAsync();
        }

        // Get a book by its ID
        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.Include(b => b.Author) // Include Author if needed
                                        .Include(b => b.Publisher) // Include Publisher if needed
                                        .FirstOrDefaultAsync(b => b.BookId == id);
        }

        // Add a new book
        public async Task AddBook(Book book)
        {
            if (await GetBookById(book.BookId) != null)
            {
                throw new Exception("Book already exists");
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        // Update an existing book
        public async Task UpdateBook(Book book)
        {
            var existingBook = await GetBookById(book.BookId);
            if (existingBook == null)
            {
                throw new Exception("Book not found");
            }

            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId; // Update AuthorId instead of Author object
            existingBook.Description = book.Description;
            existingBook.Price = book.Price;
            existingBook.Stock = book.Stock;
            existingBook.PublicationDate = book.PublicationDate;
            existingBook.Genre = book.Genre;
            existingBook.IsActive = book.IsActive;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();
        }

        // Delete a book by its ID
        public async Task DeleteBook(int id)
        {
            var book = await GetBookById(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        // Get a paginated list of books with optional search term
        public async Task<BookRespone> GetPaginatedBooks(string searchTerm, int pageIndex, int pageSize)
        {
            var query = _context.Books.Include(b => b.Author)   // Include Author if needed
                                       .Include(b => b.Publisher) // Include Publisher if needed
                                       .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Assuming Author has a FirstName and LastName property
                query = query.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                                         (b.Author != null &&
                                          (b.Author.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                                           b.Author.LastName.ToLower().Contains(searchTerm.ToLower()))) ||
                                         (b.Description != null && b.Description.ToLower().Contains(searchTerm.ToLower())));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var paginatedBooks = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new BookRespone
            {
                Books = paginatedBooks,
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }

    } 
    }
using BusinessObjects;
using BusinessObjects.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daos
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
                    instance = new AuthorDAO();
                }
                return instance;
            }
        }

        // Get all authors
        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        // Get an author by their ID
        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
        }

        // Add a new author
        public async Task AddAuthor(Author author)
        {
            if (await GetAuthorById(author.AuthorId) != null)
            {
                throw new Exception("Author already exists");
            }
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        // Update an existing author
        public async Task UpdateAuthor(Author author)
        {
            var existingAuthor = await GetAuthorById(author.AuthorId);
            if (existingAuthor == null)
            {
                throw new Exception("Author not found");
            }

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.Biography = author.Biography; // Example additional property

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();
        }

        // Delete an author by their ID
        public async Task DeleteAuthor(int id)
        {
            var author = await GetAuthorById(id);
            if (author == null)
            {
                throw new Exception("Author not found");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

        // Get a paginated list of authors with optional search term
        public async Task<AuthorResponse> GetPaginatedAuthors(string searchTerm, int pageIndex, int pageSize)
        {
            var query = _context.Authors.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(a => a.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                                         a.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                                         (a.Biography != null && a.Biography.ToLower().Contains(searchTerm.ToLower())));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var paginatedAuthors = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new AuthorResponse
            {
                Authors = paginatedAuthors,
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

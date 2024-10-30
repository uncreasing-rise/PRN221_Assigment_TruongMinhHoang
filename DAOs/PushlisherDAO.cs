using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Daos
{
    public class PublisherDAO
    {
        private readonly BookStoreContext _context;
        private static PublisherDAO _instance;

        private PublisherDAO()
        {
            _context = new BookStoreContext();
        }

        public static PublisherDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PublisherDAO();
                }
                return _instance;
            }
        }

        // Get all publishers
        public async Task<List<Publisher>> GetPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        // Get a publisher by its ID
        public async Task<Publisher> GetPublisherById(int id)
        {
            return await _context.Publishers.FindAsync(id); // Using FindAsync for efficiency
        }

        // Add a new publisher
        public async Task AddPublisher(Publisher publisher)
        {
            ValidatePublisher(publisher);

            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
        }

        // Update an existing publisher
        public async Task UpdatePublisher(Publisher publisher)
        {
            var existingPublisher = await GetPublisherById(publisher.PublisherId);
            if (existingPublisher == null)
            {
                throw new Exception("Publisher not found");
            }

            // Update existing publisher fields
            existingPublisher.PublisherName = publisher.PublisherName;
            existingPublisher.ContactEmail = publisher.ContactEmail;
            existingPublisher.WebsiteUrl = publisher.WebsiteUrl;
            existingPublisher.Address = publisher.Address;

            _context.Entry(existingPublisher).State = EntityState.Modified; // Explicitly mark the entity as modified
            await _context.SaveChangesAsync();
        }

        // Delete a publisher by its ID
        public async Task DeletePublisher(int id)
        {
            var publisher = await _context.Publishers
                .Include(p => p.Books) // Include associated books
                .FirstOrDefaultAsync(p => p.PublisherId == id);

            if (publisher == null)
            {
                throw new Exception("Publisher not found");
            }

            // Remove all associated books
            _context.Books.RemoveRange(publisher.Books);

            // Remove the publisher
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        // Validate publisher
        private void ValidatePublisher(Publisher publisher)
        {
            var validationContext = new ValidationContext(publisher);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(publisher, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ValidationException("Publisher entity is not valid: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
            }
        }
    }
}

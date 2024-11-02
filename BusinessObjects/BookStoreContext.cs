using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        private string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Could not find a connection string named 'DefaultConnectionString'.");
            }

            return connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("PK_Account");

                entity.ToTable("Account");

                entity.HasIndex(e => e.Username).IsUnique().HasName("UQ_Account_Username");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.Role).HasMaxLength(50);
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AuthorId).HasName("PK_Author");

                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
                entity.Property(e => e.Biography).HasColumnType("text");
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId).HasName("PK_Book");

                entity.ToTable("Book");

                entity.HasIndex(e => e.AuthorId).HasName("IX_Book_AuthorId");
                entity.HasIndex(e => e.PublisherId).HasName("IX_Book_PublisherId");
                entity.HasIndex(e => e.Isbn).IsUnique().HasName("UQ_Book_ISBN")
                    .HasFilter("([ISBN] IS NOT NULL)");

                entity.Property(e => e.BookId).HasColumnName("BookID").ValueGeneratedOnAdd();
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Genre).HasMaxLength(50);
                entity.Property(e => e.IsActive).HasDefaultValue(1);
                entity.Property(e => e.Isbn).HasMaxLength(13).HasColumnName("ISBN");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Book_Author");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK_Books_Publishers");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK_Customer");

                entity.ToTable("Customer");

                entity.HasIndex(e => e.AccountId).IsUnique().HasName("UQ_Customer_AccountID")
                    .HasFilter("([AccountID] IS NOT NULL)");
                entity.HasIndex(e => e.Email).IsUnique().HasName("UQ_Customer_Email")
                    .HasFilter("([Email] IS NOT NULL)");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.AccountId).HasColumnName("AccountID");
                entity.Property(e => e.Address).HasColumnType("text");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.Account)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.AccountId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Customer_Account");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK_Order");

                entity.ToTable("Order");

                entity.HasIndex(e => e.CustomerId).HasName("IX_Order_CustomerID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.OrderDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(50)
                    .HasDefaultValue("Pending");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId).HasName("PK_OrderItem");

                entity.ToTable("OrderItem");

                entity.HasIndex(e => e.BookId).HasName("IX_OrderItem_BookID");
                entity.HasIndex(e => e.OrderId).HasName("IX_OrderItem_OrderID");

                entity.Property(e => e.OrderItemId).HasColumnName("OrderItemID");
                entity.Property(e => e.BookId).HasColumnName("BookID");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_OrderItem_Book");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItem_Order");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.PublisherId).HasName("PK_Publisher");

                entity.ToTable("Publisher");

                entity.HasIndex(e => e.ContactEmail).IsUnique().HasName("UQ_Publisher_ContactEmail")
                    .HasFilter("([ContactEmail] IS NOT NULL)");

                entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
                entity.Property(e => e.Address).HasColumnType("text");
                entity.Property(e => e.ContactEmail).HasMaxLength(255);
                entity.Property(e => e.PublisherName).HasMaxLength(100);
                entity.Property(e => e.WebsiteUrl).HasMaxLength(255).HasColumnName("WebsiteURL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

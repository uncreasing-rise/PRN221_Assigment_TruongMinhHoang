using System.Collections.Generic;
using System.Windows;
using BusinessObjects;
using Repositories; // Ensure you have this namespace for the repository

namespace BookManageWPF
{
    public partial class MainWindow : Window
    {
        private readonly IBookRepo _bookRepo; // Dependency Injection for the book repository
        private readonly IPublisherRepo _publisherRepo; // Repository for Publisher
        private readonly IAuthorRepo _authorRepo; // Repository for Author
        private List<Book> _books; // List of books

        public MainWindow(IBookRepo bookRepo, IPublisherRepo publisherRepo, IAuthorRepo authorRepo)
        {
            InitializeComponent();
            _bookRepo = bookRepo;
            _publisherRepo = publisherRepo; // Initialize Publisher repository
            _authorRepo = authorRepo; // Initialize Author repository
            LoadPublishers(); // Load the list of Publishers
            LoadAuthors(); // Load the list of Authors
            LoadBooks(); // Load the list of books on startup
        }

        private async void LoadPublishers()
        {
            var publishers = await _publisherRepo.GetPublishers(); // Assume you have this method
            PublisherComboBox.ItemsSource = publishers; // Bind the list of publishers to the ComboBox
        }

        private async void LoadAuthors()
        {
            var authors = await _authorRepo.GetAuthors(); // Assume you have this method
            AuthorComboBox.ItemsSource = authors; // Bind the list of authors to the ComboBox
        }

        private async void LoadBooks()
        {
            _books = await _bookRepo.GetList(); // Get all books from the repository
            BooksListView.ItemsSource = _books; // Bind the list of books to the ListView
        }

        private async void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(BookTitleTextBox.Text) ||
                AuthorComboBox.SelectedItem == null || // Check if an author is selected
                string.IsNullOrWhiteSpace(ISBNTextBox.Text) ||
                string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                string.IsNullOrWhiteSpace(StockTextBox.Text) ||
                PublisherComboBox.SelectedItem == null) // Check if a publisher is selected
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            // Create a new book
            var newBook = new Book
            {
                Title = BookTitleTextBox.Text,
                AuthorId = ((Author)AuthorComboBox.SelectedItem).AuthorId, // Get AuthorId from ComboBox
                Isbn = ISBNTextBox.Text,
                Price = decimal.Parse(PriceTextBox.Text),
                Stock = int.Parse(StockTextBox.Text),
                PublisherId = ((Publisher)PublisherComboBox.SelectedItem).PublisherId // Get PublisherId from ComboBox
            };

            await _bookRepo.AddBook(newBook); // Add the book to the repository
            LoadBooks(); // Reload the list of books

            // Clear input fields
            BookTitleTextBox.Clear();
            AuthorComboBox.SelectedItem = null; // Reset Author ComboBox
            ISBNTextBox.Clear();
            PriceTextBox.Clear();
            StockTextBox.Clear();
            PublisherComboBox.SelectedItem = null; // Reset Publisher ComboBox
        }
    }
}

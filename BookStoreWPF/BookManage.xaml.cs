using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookStoreWPF
{
    public partial class BookManage : Window
    {
        private readonly IBookRepo _bookRepo;
        private readonly IPublisherRepo _publisherRepo;
        private readonly IAuthorRepo _authorRepo;
        private List<Book> _books;

        public BookManage(IBookRepo bookRepo, IPublisherRepo publisherRepo, IAuthorRepo authorRepo)
        {
            InitializeComponent();
            _bookRepo = bookRepo;
            _publisherRepo = publisherRepo;
            _authorRepo = authorRepo;

            // Load publishers and authors in parallel
            Task.Run(async () =>
            {
                await LoadPublishers();
                await LoadAuthors();
                await LoadBooks();
            });
        }

        private async Task LoadPublishers()
        {
            try
            {
                var publishers = await _publisherRepo.GetPublishers();
                Dispatcher.Invoke(() => PublisherComboBox.ItemsSource = publishers?.ToList() ?? new List<Publisher>());
            }
            catch (Exception ex)
            {
                ShowError("Error loading publishers: " + ex.Message);
            }
        }

        private async Task LoadAuthors()
        {
            try
            {
                var authors = await _authorRepo.GetAuthors();
                Dispatcher.Invoke(() => AuthorComboBox.ItemsSource = authors?.ToList() ?? new List<Author>());
            }
            catch (Exception ex)
            {
                ShowError("Error loading authors: " + ex.Message);
            }
        }

        private async Task LoadBooks()
        {
            try
            {
                _books = await _bookRepo.GetList();
                Dispatcher.Invoke(() => BooksListView.ItemsSource = _books);
            }
            catch (Exception ex)
            {
                ShowError("Error loading books: " + ex.Message);
            }
        }

        private async void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            var newBook = new Book
            {
                Title = BookTitleTextBox.Text,
                AuthorId = ((Author)AuthorComboBox.SelectedItem).AuthorId,
                Isbn = ISBNTextBox.Text,
                Price = decimal.Parse(PriceTextBox.Text),
                Stock = int.Parse(StockTextBox.Text),
                PublisherId = ((Publisher)PublisherComboBox.SelectedItem).PublisherId
            };

            try
            {
                await _bookRepo.AddBook(newBook);
                await LoadBooks(); // Re-fetch books after adding
                ClearInputFields();
            }
            catch (Exception ex)
            {
                ShowError("Error adding book: " + ex.Message);
            }
        }

        private async void BooksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksListView.SelectedItem is Book selectedBook)
            {
                BookTitleTextBox.Text = selectedBook.Title;
                ISBNTextBox.Text = selectedBook.Isbn;
                PriceTextBox.Text = selectedBook.Price.ToString("F2");
                StockTextBox.Text = selectedBook.Stock.ToString();

                // Load selected author and publisher asynchronously
                var authors = await _authorRepo.GetAuthors();
                var publishers = await _publisherRepo.GetPublishers();

                AuthorComboBox.SelectedItem = authors.FirstOrDefault(a => a.AuthorId == selectedBook.AuthorId);
                PublisherComboBox.SelectedItem = publishers.FirstOrDefault(p => p.PublisherId == selectedBook.PublisherId);

            }
        }

        private async void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksListView.SelectedItem is Book selectedBook && ValidateInputs())
            {
                selectedBook.Title = BookTitleTextBox.Text;
                selectedBook.AuthorId = ((Author)AuthorComboBox.SelectedItem).AuthorId;
                selectedBook.Isbn = ISBNTextBox.Text;
                selectedBook.Price = decimal.Parse(PriceTextBox.Text);
                selectedBook.Stock = int.Parse(StockTextBox.Text);
                selectedBook.PublisherId = ((Publisher)PublisherComboBox.SelectedItem).PublisherId;

                try
                {
                    await _bookRepo.UpdateBook(selectedBook);
                    await LoadBooks(); // Re-fetch books after updating
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    ShowError("Error updating book: " + ex.Message);
                }
            }
        }

        private async void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksListView.SelectedItem is Book selectedBook)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{selectedBook.Title}'?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _bookRepo.DeleteBook(selectedBook.BookId);
                        await LoadBooks(); // Re-fetch books after deletion
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error deleting book: " + ex.Message);
                    }
                }
            }
        }

        private void ClearInputFields()
        {
            BookTitleTextBox.Clear();
            AuthorComboBox.SelectedItem = null;
            ISBNTextBox.Clear();
            PriceTextBox.Clear();
            StockTextBox.Clear();
            PublisherComboBox.SelectedItem = null;
            BooksListView.SelectedItem = null;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(BookTitleTextBox.Text))
            {
                ShowError("Title cannot be empty.");
                return false;
            }
            if (AuthorComboBox.SelectedItem == null)
            {
                ShowError("Please select an author.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(ISBNTextBox.Text))
            {
                ShowError("ISBN cannot be empty.");
                return false;
            }
            if (!decimal.TryParse(PriceTextBox.Text, out _))
            {
                ShowError("Price must be a valid number.");
                return false;
            }
            if (!int.TryParse(StockTextBox.Text, out _))
            {
                ShowError("Stock must be a valid integer.");
                return false;
            }
            if (PublisherComboBox.SelectedItem == null)
            {
                ShowError("Please select a publisher.");
                return false;
            }
            return true;
        }
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

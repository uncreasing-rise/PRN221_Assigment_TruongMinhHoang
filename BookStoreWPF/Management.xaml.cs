using Repositories;
using System.Windows;

namespace BookStoreWPF
{
    public partial class Management : Window
    {
        private readonly IBookRepo _bookRepo;
        private readonly IPublisherRepo _publisherRepo;
        private readonly IAuthorRepo _authorRepo;

        public Management(IBookRepo bookRepo, IPublisherRepo publisherRepo, IAuthorRepo authorRepo)
        {
            _bookRepo = bookRepo;
            _publisherRepo = publisherRepo;
            _authorRepo = authorRepo;
        }
        public Management()
        {
            InitializeComponent(); // This is required to initialize the XAML components.
        }
        private void ManageBooks_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of BookManage with the necessary dependencies
            BookManage bookManage = new BookManage(_bookRepo, _publisherRepo, _authorRepo);
            bookManage.Show();
        }

        private void ManageAuthors_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of AuthorManage with the necessary dependencies
            AuthorManage authorManage = new AuthorManage(_authorRepo);
            authorManage.Show();
        }

        private void ManagePublishers_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of PublisherManage with the necessary dependencies
            PublisherManage publisherManage = new PublisherManage(_publisherRepo);
            publisherManage.Show();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }
    }
}

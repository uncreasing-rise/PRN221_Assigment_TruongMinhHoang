using Repositories;
using System;
using System.Windows;

namespace BookStoreWPF
{
    public partial class MainWindow : Window
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IBookRepo _bookRepo; // Add Book repository
        private readonly IPublisherRepo _publisherRepo; // Add Publisher repository
        private readonly IAuthorRepo _authorRepo; // Add Author repository

        // Default constructor
        public MainWindow()
        {
            InitializeComponent();
            _accountRepo = new AccountRepo(); // Initialize your account repository here
            _bookRepo = new BookRepo(); // Initialize your book repository here
            _publisherRepo = new PublisherRepo(); // Initialize your publisher repository here
            _authorRepo = new AuthorRepo(); // Initialize your author repository here
        }

        // Constructor with dependency injection
        public MainWindow(IAccountRepo accountRepo, IBookRepo bookRepo, IPublisherRepo publisherRepo, IAuthorRepo authorRepo)
        {
            InitializeComponent();
            _accountRepo = accountRepo;
            _bookRepo = bookRepo;
            _publisherRepo = publisherRepo;
            _authorRepo = authorRepo;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from the controls
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Validate input
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageTextBlock.Text = "Username and password cannot be empty.";
                return;
            }

            try
            {
                var account = await _accountRepo.Login(username, password);
                if (account != null)
                {
                    MessageTextBlock.Text = "Login successful!";

                    // Check if the user role is Admin
                   
                        if (account.Role == "Admin") // Assuming Role is a property of account
                        {
                        // Create an instance of Management window
                        Management managementWindow = new Management(_bookRepo, _publisherRepo, _authorRepo);

                        // Show the Management window
                        managementWindow.Show();

                            // Close the current window
                            this.Close();
                        }
                        else
                        {
                            MessageTextBlock.Text = "Access denied. Admin role required.";
                        
                    }

                }
                else
                {
                    MessageTextBlock.Text = "Invalid username or password.";
                }
            }
            catch (Exception ex)
            {
                MessageTextBlock.Text = $"Error: {ex.Message}"; // Display error message
            }
        }
    }
}

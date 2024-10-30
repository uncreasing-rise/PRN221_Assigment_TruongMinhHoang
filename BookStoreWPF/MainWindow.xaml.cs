using BusinessObjects;
using Daos;
using Repositories;
using System.Windows;

namespace BookStoreWPF
{
    public partial class MainWindow : Window
    {
        private readonly IAccountRepo _accountRepo;

        public MainWindow()
        {
            InitializeComponent();
            _accountRepo = new AccountRepo(); // Initialize your repository
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            Account account = await _accountRepo.Login(username, password);

            if (account != null && account.Role.Equals("Admin"))
            {
                ErrorMessageTextBlock.Text = "Login successful!";
            }
            else
            {
                ErrorMessageTextBlock.Text = "Invalid username or password.";
            }
        }
    }
}

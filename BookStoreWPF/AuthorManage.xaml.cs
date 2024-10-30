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
    public partial class AuthorManage : Window
    {
        private readonly IAuthorRepo _authorRepo;
        private List<Author> _authors;

        public AuthorManage(IAuthorRepo authorRepo)
        {
            InitializeComponent();
            _authorRepo = authorRepo;

            // Load authors asynchronously
            LoadAuthorsAsync();
        }

        private async Task LoadAuthorsAsync()
        {
            try
            {
                _authors = await _authorRepo.GetAuthors();
                AuthorsListView.ItemsSource = _authors; // Directly set ItemsSource on the UI thread
            }
            catch (Exception ex)
            {
                ShowError("Error loading authors: " + ex.Message);
            }
        }

        private async void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var newAuthor = new Author
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Biography = BiographyTextBox.Text // Include Biography
            };

            try
            {
                await _authorRepo.AddAuthor(newAuthor);
                await LoadAuthorsAsync(); // Re-fetch authors after adding
                ClearInputFields();
            }
            catch (Exception ex)
            {
                ShowError("Error adding author: " + ex.Message);
            }
        }

        private void AuthorsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorsListView.SelectedItem is Author selectedAuthor)
            {
                FirstNameTextBox.Text = selectedAuthor.FirstName;
                LastNameTextBox.Text = selectedAuthor.LastName;
                BiographyTextBox.Text = selectedAuthor.Biography; // Load Biography
            }
            else
            {
                ClearInputFields(); // Clear fields if no selection
            }
        }

        private async void UpdateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsListView.SelectedItem is Author selectedAuthor)
            {
                selectedAuthor.FirstName = FirstNameTextBox.Text;
                selectedAuthor.LastName = LastNameTextBox.Text;
                selectedAuthor.Biography = BiographyTextBox.Text; // Update Biography

                try
                {
                    await _authorRepo.UpdateAuthor(selectedAuthor);
                    await LoadAuthorsAsync(); // Re-fetch authors after updating
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    ShowError("Error updating author: " + ex.Message);
                }
            }
        }

        private async void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthorsListView.SelectedItem is Author selectedAuthor)
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{selectedAuthor.FirstName} {selectedAuthor.LastName}'?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _authorRepo.DeleteAuthor(selectedAuthor.AuthorId);
                        await LoadAuthorsAsync(); // Re-fetch authors after deletion
                        ClearInputFields();
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error deleting author: " + ex.Message);
                    }
                }
            }
        }

        private void ClearInputFields()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            BiographyTextBox.Clear(); // Clear Biography field
            AuthorsListView.SelectedItem = null;
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects; // Ensure you have the correct namespace for your BusinessObjects
using Repositories;    // Ensure you have the correct namespace for your Repositories

namespace BookStoreWPF
{
    /// <summary>
    /// Interaction logic for PublisherManage.xaml
    /// </summary>
    public partial class PublisherManage : Window
    {
        private readonly IPublisherRepo _publisherRepo; // Repository for Publisher
        private List<Publisher> _publishers; // List to hold publishers

        public PublisherManage(IPublisherRepo publisherRepo)
        {
            InitializeComponent();
            _publisherRepo = publisherRepo; // Initialize repository
            LoadPublishers(); // Load publishers when the window initializes
        }

        private async void LoadPublishers()
        {
            // Load publishers asynchronously from the repository and bind to ListView
            _publishers = await _publisherRepo.GetPublishers(); // Await the asynchronous call
            PublishersListView.ItemsSource = _publishers; // Bind to the ListView
        }


        private void AddPublisherButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new publisher object and fill it with data from the input fields
            var newPublisher = new Publisher
            {
                PublisherName = PublisherNameTextBox.Text,
                ContactEmail = ContactEmailTextBox.Text,
                WebsiteUrl = WebsiteUrlTextBox.Text,
                Address = AddressTextBox.Text
            };

            // Add the new publisher to the repository
            _publisherRepo.AddPublisher(newPublisher);
            LoadPublishers(); // Refresh the publisher list
            ClearInputFields(); // Clear the input fields
        }

        private void UpdatePublisherButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a publisher is selected in the ListView
            if (PublishersListView.SelectedItem is Publisher selectedPublisher)
            {
                // Update the publisher details with input field values
                selectedPublisher.PublisherName = PublisherNameTextBox.Text;
                selectedPublisher.ContactEmail = ContactEmailTextBox.Text;
                selectedPublisher.WebsiteUrl = WebsiteUrlTextBox.Text;
                selectedPublisher.Address = AddressTextBox.Text;

                // Update the publisher in the repository
                _publisherRepo.UpdatePublisher(selectedPublisher);
                LoadPublishers(); // Refresh the publisher list
                ClearInputFields(); // Clear the input fields
            }
            else
            {
                MessageBox.Show("Please select a publisher to update.");
            }
        }

        private void DeletePublisherButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if a publisher is selected in the ListView
            if (PublishersListView.SelectedItem is Publisher selectedPublisher)
            {
                // Delete the publisher from the repository
                _publisherRepo.DeletePublisher(selectedPublisher.PublisherId);
                LoadPublishers(); // Refresh the publisher list
                ClearInputFields(); // Clear the input fields
            }
            else
            {
                MessageBox.Show("Please select a publisher to delete.");
            }
        }

        private void PublishersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Populate input fields with the selected publisher's details
            if (PublishersListView.SelectedItem is Publisher selectedPublisher)
            {
                PublisherNameTextBox.Text = selectedPublisher.PublisherName;
                ContactEmailTextBox.Text = selectedPublisher.ContactEmail;
                WebsiteUrlTextBox.Text = selectedPublisher.WebsiteUrl;
                AddressTextBox.Text = selectedPublisher.Address;
            }
        }

        private void ClearInputFields()
        {
            // Clear the input fields after adding or updating a publisher
            PublisherNameTextBox.Clear();
            ContactEmailTextBox.Clear();
            WebsiteUrlTextBox.Clear();
            AddressTextBox.Clear();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Close the window
            this.Close();
        }
    }
}

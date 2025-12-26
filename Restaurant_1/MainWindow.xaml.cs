using System;
using System.Windows;
using Restaurant_1.Classes;

namespace Restaurant_1
{

    public partial class MainWindow : Window
    {
        private readonly Employee _employee = new Employee();
        private object? _currentOrder;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitNewRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (QuantityTextBox.Text == "0")
                {
                    throw new Exception("Please, make a NEW order!");
                }

                // Parse quantity and determine menu item
                int quantity = int.Parse(QuantityTextBox.Text);
                string menuItem = ChickenRadioButton.IsChecked == true
                    ? "chicken"
                    : "egg";

                // Create new request
                _currentOrder = _employee.NewRequest(quantity, menuItem);

                // Inspect the order
                string inspectionResult = _employee.Inspect(_currentOrder);

                // Display result
                ResultsTextBox.Text = $"New request created. {inspectionResult}";
                QuantityTextBox.Text = "0";
            }
            catch (Exception ex)
            {
                _currentOrder = null;
                ResultsTextBox.Text = ex.Message;
            }
        }

        private void CopyPreviousRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Copy and inspect the previous order
                _currentOrder = _employee.CopyRequest();
                string inspectionResult = _employee.Inspect(_currentOrder);

                ResultsTextBox.Text = $"Copied previous order.\n{inspectionResult}";
            }
            catch (Exception)
            {
                ResultsTextBox.Text =
                    "An error occurred! Cannot copy previous order.";
            }
        }

        private void PrepareFoodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentOrder == null)
                {
                    ResultsTextBox.Text = "No order to prepare.";
                    return;
                }

                string result = _employee.PrepareFood(_currentOrder);
                ResultsTextBox.Text = result;
            }
            catch (Exception ex)
            {
                ResultsTextBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}

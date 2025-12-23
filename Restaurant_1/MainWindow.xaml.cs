using Restaurant_1.Classes;
using System.Windows;

namespace Restaurant_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Employee employee = new();
        private object? currentOrder = null;
        private bool _isPrepared = false;
        private void SubmitNewRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (QuantityTextBox.Text == "0")
                    throw new Exception("Please, make a NEW order!");

                // get quantity and type of food
                int quantity = int.Parse(QuantityTextBox.Text);
                string menuItem = ChickenRadioButton.IsChecked == true ? "chicken" : "egg";

                // employee makes new request
                currentOrder = employee.NewRequest(quantity, menuItem);

                // Inspect
                string inspect = employee.Inspect(currentOrder);

                // fill result box with current order status
                ResultsTextBox.Text = $"New request created. {inspect}";
                QuantityTextBox.Text = "0";
            }

            catch (Exception ex)
            {
                currentOrder = null;
                ResultsTextBox.Text = ex.Message;
            }
        }

        private void CopyPreviousRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_isPrepared)
                {
                    // copy and inspect new order with the same amount of food
                    currentOrder = employee.CopyRequest();
                    string inspect = employee.Inspect(currentOrder);

                    ResultsTextBox.Text = $"Copied previous order. \n {inspect}";
                }
                else
                {
                    ResultsTextBox.Text = $"NO last order to copy";
                }
            }

            catch (Exception)
            {
                ResultsTextBox.Text = "An error occuried! Can not copy previous order.";
            }
        }

        private void PrepareFoodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentOrder == null)
                {
                    ResultsTextBox.Text = "No order to prepare.";
                    return;
                }

                string result = employee.PrepareFood(currentOrder);
                ResultsTextBox.Text = result;
            }

            catch (Exception ex)
            {
                ResultsTextBox.Text = "Error: " + ex.Message;
            }
            _isPrepared = true;
        }
    }
}
using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Windows;
using VoNgocTrucLamWPF.Helper;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for CustomerInfoMngWindow.xaml
    /// </summary>
    public partial class CustomerInfoMngWindow : Window
    {
        IUnitOfWork unitOfWork;
        public Customer Customer { get; set; }
        public CustomerInfoMngWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(unitOfWork);
            loginWindow.Show();
            this.Close();
        }
        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderMngWindow = new CustomerOrderMngWindow(unitOfWork)
            {
                Customer = this.Customer
            };
            orderMngWindow.Show();
            this.Close();
        }
        private void LoadCustomerInfo()
        {
            txtCustomerId.Text = Customer.CustomerId.ToString();
            txtEmail.Text = Customer.Email;
            txtCustomerName.Text = Customer.CustomerName;
            txtCity.Text = Customer.City;
            txtCountry.Text = Customer.Country;
            dtpBirthday.Text = Customer.Birthday.ToString();
            txtPassword.Password = Customer.Password;
            txtConfirmPass.Password = Customer.Password;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomerInfo();
            lbUsername.Text = Customer.CustomerName;
        }
        private Customer GetCustomerObject()
        {
            Customer customer = null;
            try
            {
                customer = new Customer
                {
                    CustomerId = int.Parse(txtCustomerId.Text.Trim()),
                    Email = txtEmail.Text.Trim(),
                    CustomerName = txtCustomerName.Text.Trim(),
                    Birthday = DateTime.Parse(dtpBirthday.SelectedDate.Value.Date.ToShortDateString()),
                    City = txtCity.Text.Trim(),
                    Country = txtCountry.Text.Trim(),
                    Password = txtPassword.Password
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Customer Object");
            }
            return customer;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidate())
            {
                var customer = GetCustomerObject();
                unitOfWork.CustomerRepository.Update(customer);
                Customer = customer;
                LoadCustomerInfo();
                MessageBox.Show($"Your information has been updated successfully!", "Update Customer Info");
            }
        }
        private bool CheckValidate()
        {
            if (txtCustomerId.Text.Length == 0 || txtEmail.Text.Length == 0 || txtCustomerName.Text.Length == 0
                || txtPassword.Password.Length == 0 || txtConfirmPass.Password.Length == 0
                || txtCity.Text.Length == 0 || txtCountry.Text.Length == 0 ||
                !dtpBirthday.SelectedDate.HasValue)
            {
                MessageBox.Show("Input fields must not be null!", "Input Customer Info");
                return false;
            }
            if (!txtPassword.Password.Equals(txtConfirmPass.Password))
            {
                MessageBox.Show("Password and confirm password are not match!", "Input Customer Info");
                return false;
            }
            if (!txtCustomerId.Text.IsNumeric())
            {
                MessageBox.Show("ID wrong format! (must be an integer)", "Input Customer Info");
                return false;
            }
            return true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerInfo();
        }
    }
}

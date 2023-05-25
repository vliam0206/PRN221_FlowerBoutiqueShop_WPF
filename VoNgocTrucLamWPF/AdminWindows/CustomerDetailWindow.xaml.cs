using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Windows;
using VoNgocTrucLamWPF.Helper;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for CustomerDetailWindow.xaml
    /// </summary>
    public partial class CustomerDetailWindow : Window
    {
        IUnitOfWork unitOfWork;
        public bool InsertOrUpdate { get; set; } // true: insert, false: update
        public Customer customerOject { get; set; }
        public CustomerDetailWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // assign update information
            if (!InsertOrUpdate && customerOject != null) //update
            {
                var cus = unitOfWork.CustomerRepository.GetCustomer(customerOject.CustomerId);
                txtCustomerId.Text = customerOject.CustomerId.ToString();
                txtEmail.Text = customerOject.Email;
                txtCustomerName.Text = customerOject.CustomerName;
                txtCity.Text = customerOject.City;
                txtCountry.Text = customerOject.Country;
                dtpBirthday.Text = customerOject.Birthday.ToString();
                txtPassword.Password = cus.Password;
                txtConfirmPass.Password = cus.Password;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InsertOrUpdate)
                {
                    InsertCustomer();
                }
                else
                {
                    UpdateCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save customer");
                this.DialogResult = false;
            }
        }
        private Customer GetCustomerObject()
        {
            Customer customer = null;
            var date = dtpBirthday.SelectedDate.HasValue ? dtpBirthday.SelectedDate.Value.Date.ToShortDateString() : null;
            try
            {
                customer = new Customer
                {
                    CustomerId = int.Parse(txtCustomerId.Text.Trim()),
                    Email = txtEmail.Text.Trim(),
                    CustomerName = txtCustomerName.Text.Trim(),
                    Birthday = DateTime.Parse(date),
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
        private void InsertCustomer()
        {
            if (CheckValidate())
            {
                Customer customerObj = GetCustomerObject();
                unitOfWork.CustomerRepository.Insert(customerObj);
                MessageBox.Show($"Customer \"{customerObj.CustomerName}\" has been added successfully!", "Add Succeed");
                this.DialogResult = true;
            }
        }
        private void UpdateCustomer()
        {
            if (CheckValidate())
            {
                Customer customerObj = GetCustomerObject();
                unitOfWork.CustomerRepository.Update(customerObj);
                MessageBox.Show($"Customer \"{customerObj.CustomerName}\" has been updated successfully!", "Update Succeed");
                this.DialogResult = true;
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
    }
}

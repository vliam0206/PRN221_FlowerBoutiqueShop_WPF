using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Windows;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for AdminCustomerMngWindow.xaml
    /// </summary>
    public partial class AdminCustomerMngWindow : Window
    {
        IUnitOfWork unitOfWork;
        public AdminCustomerMngWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            LoadCustomerList();
        }
        private Customer GetCustomerObj()
        {
            if (lvCustomerData.SelectedIndex == -1)
            {
                throw new Exception("No item selected!");
            }
            Customer customer = null;
            try
            {
                customer = (Customer)lvCustomerData.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Customer Object");
            }
            return customer;
        }
        public void LoadCustomerList()
        {
            lvCustomerData.ItemsSource = unitOfWork.CustomerRepository.GetAll();
            txtSearch.Text = string.Empty;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(unitOfWork);
            loginWindow.Show();
            this.Close();
        }

        private void btnFlower_Click(object sender, RoutedEventArgs e)
        {
            var flowerMngWindow = new AdminFlowerMngWindow(unitOfWork);
            flowerMngWindow.Show();
            this.Close();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderMngWindow = new AdminOrderMngWindow(unitOfWork);
            orderMngWindow.Show();
            this.Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomerList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerDetailWindow detailWindow = new CustomerDetailWindow(unitOfWork)
                {
                    Title = "Add Customer",
                    InsertOrUpdate = true
                };
                detailWindow.ShowInTaskbar = false;
                detailWindow.Owner = this;
                if (detailWindow.ShowDialog().Value)
                {
                    LoadCustomerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Customer Failed");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customerObj = GetCustomerObj();
                CustomerDetailWindow detailWindow = new CustomerDetailWindow(unitOfWork)
                {
                    Title = "Update Customer",
                    InsertOrUpdate = false,
                    customerOject = customerObj
                };
                detailWindow.ShowInTaskbar = false;
                detailWindow.Owner = this;
                if (detailWindow.ShowDialog().Value)
                {
                    LoadCustomerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Customer failed");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customerObj = GetCustomerObj();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete customer \"{customerObj.CustomerName}\"?",
                    "Delete Customer", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    unitOfWork.CustomerRepository.Delete(customerObj);
                    MessageBox.Show($"Customer \"{customerObj.CustomerName}\" has been deleted successfully!", "Delete Succeed");
                    LoadCustomerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Customer Failed");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var customerList = unitOfWork.CustomerRepository.Search(txtSearch.Text.Trim());
            lvCustomerData.ItemsSource = customerList;
        }
    }
}

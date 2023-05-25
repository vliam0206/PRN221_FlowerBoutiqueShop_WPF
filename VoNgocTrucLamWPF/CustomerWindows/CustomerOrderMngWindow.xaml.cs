using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Windows;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for CustomerOrderMngWindow.xaml
    /// </summary>
    public partial class CustomerOrderMngWindow : Window
    {
        IUnitOfWork unitOfWork;
        public Customer Customer { get; set; }
        public CustomerOrderMngWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbUsername.Text = Customer.CustomerName;
            LoadOrderList();
        }
        private void LoadOrderList()
        {
            lvOrderData.ItemsSource = unitOfWork.OrderRepository.GetOrders(Customer.CustomerId);
        }
        private Order GetOrderObj()
        {
            if (lvOrderData.SelectedIndex == -1)
            {
                throw new Exception("No item selected!");
            }
            Order order = null;
            try
            {
                order = (Order)lvOrderData.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order Object");
            }
            return order;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(unitOfWork);
            loginWindow.Show();
            this.Close();
        }

        private void btnInformation_Click(object sender, RoutedEventArgs e)
        {
            var inforMngWindow = new CustomerInfoMngWindow(unitOfWork)
            {
                Customer = this.Customer
            };
            inforMngWindow.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue)
            {
                var orderList = unitOfWork.OrderRepository.GetOrders(Customer.CustomerId);
                var startDate = dpStartDate.SelectedDate.Value;
                var endDate = dpEndDate.SelectedDate.Value;
                var searchOrders = unitOfWork.OrderRepository
                    .GetOrdersByOrderDate(orderList, startDate, endDate);
                lvOrderData.ItemsSource = searchOrders;
            } else
            {
                LoadOrderList();
                MessageBox.Show("Input fields must not be empty!", "Search Order");
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = GetOrderObj();
                OrderDetailWindow detailWindow = new OrderDetailWindow(unitOfWork)
                {
                    Order = order
                };
                detailWindow.ShowInTaskbar = false;
                detailWindow.Owner = this;
                detailWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Detail Order");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderList();
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
        }
    }
}

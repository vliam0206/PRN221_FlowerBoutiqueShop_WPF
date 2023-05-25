using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Windows;
using VoNgocTrucLamWPF.AdminWindows;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for AdminOrderMngWindow.xaml
    /// </summary>
    public partial class AdminOrderMngWindow : Window
    {
        IUnitOfWork unitOfWork;
        public AdminOrderMngWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadOrderList();
        }
        private void LoadOrderList()
        {
            lvOrderData.ItemsSource = unitOfWork.OrderRepository.GetAll();
            dpStartDate.Text = string.Empty;
            dpEndDate.Text = string.Empty;
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

        private void btnFlower_Click(object sender, RoutedEventArgs e)
        {
            var flowerMngWindow = new AdminFlowerMngWindow(unitOfWork);
            flowerMngWindow.Show();
            this.Close();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customerMngWindow = new AdminCustomerMngWindow(unitOfWork);
            customerMngWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue)
            {
                var startDate = dpStartDate.SelectedDate.Value;
                var endDate = dpEndDate.SelectedDate.Value;
                var orderList = unitOfWork.OrderRepository.GetOrdersByOrderDate(startDate, endDate);
                lvOrderData.ItemsSource = orderList;
            } else
            {
                MessageBox.Show("Input fields must not be empty!", "Report Statistic Failed");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = GetOrderObj();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete order " +
                    $"\"{order.OrderId}\"?", "Delete Order", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    order.OrderStatus = "Deleted";
                    unitOfWork.OrderRepository.Update(order);
                    MessageBox.Show($"Order status of order \"{order.OrderId}\" has been change to \"Deleted\"", "Delete Succeed");
                    LoadOrderList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Order");
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order ordObj = GetOrderObj();
                OrderUpdateWindow detailWindow = new OrderUpdateWindow(unitOfWork)
                {
                    OrderObject = ordObj
                };
                detailWindow.ShowInTaskbar = false;
                detailWindow.Owner = this;
                if (detailWindow.ShowDialog().Value)
                {
                    LoadOrderList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Order failed");
            }
        }
    }
}

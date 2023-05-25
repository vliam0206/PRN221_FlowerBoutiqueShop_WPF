using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VoNgocTrucLamWPF.AdminWindows
{
    /// <summary>
    /// Interaction logic for OrderUpdateWindow.xaml
    /// </summary>
    public partial class OrderUpdateWindow : Window
    {
        IUnitOfWork unitOfWork;
        public Order OrderObject { get; set; }
        public OrderUpdateWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            cboCustomer.ItemsSource = unitOfWork.CustomerRepository.GetAll();
            cboCustomer.DisplayMemberPath = "CustomerName";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtOrderId.Text = OrderObject.OrderId.ToString();
            cboCustomer.Text = unitOfWork.CustomerRepository
                    .GetCustomer(OrderObject.CustomerId.Value).CustomerName;
            dpOrderDate.Text = OrderObject.OrderDate.ToString();
            dpShippedDate.Text = OrderObject.ShippedDate.ToString();
            txtStatus.Text = OrderObject.OrderStatus;
        }
        private Order GetOrderObject()
        {
            Order order = null;
            try
            {
                order = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    CustomerId = cboCustomer.SelectedIndex != -1 ? ((Customer)cboCustomer.SelectedItem).CustomerId : -1,
                    OrderDate = dpOrderDate.SelectedDate.Value,
                    ShippedDate = dpShippedDate.SelectedDate.Value,
                    OrderStatus = txtStatus.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order Object");
            }
            return order;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidate())
            {
                Order orderObj = GetOrderObject();
                unitOfWork.OrderRepository.Update(orderObj);
                MessageBox.Show($"Order \"{orderObj.Total}\" has been updated successfully!", "Update Succeed");
                this.DialogResult = true;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool CheckValidate()
        {
            return true;
        }
    }
}

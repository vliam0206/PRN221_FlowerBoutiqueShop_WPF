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

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for OrderDetailWindow.xaml
    /// </summary>
    public partial class OrderDetailWindow : Window
    {
        IUnitOfWork unitOfWork;
        public Order Order { get; set; }
        public OrderDetailWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Order != null)
            {
                lvOrderDetail.ItemsSource = unitOfWork.OrderDetailRepository
                    .GetDetailsByOrder(Order.OrderId);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using Repositories.Cart;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IUnitOfWork unitOfWork;
        CartObject cartObject;
        public CartWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;

            cboItem.ItemsSource = unitOfWork.FlowerBouquetRepository.GetAll();
            cboItem.DisplayMemberPath = "FlowerBouquetName";

            cartObject = new CartObject();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cartObject.Cart.Count > 0)
            {

            }
        }

        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit Cart Window?", "Cart Detail",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}

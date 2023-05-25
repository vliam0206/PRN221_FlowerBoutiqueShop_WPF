using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Linq;
using System.Windows;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for AdminFlowerMngWindow.xaml
    /// </summary>
    public partial class AdminFlowerMngWindow : Window
    {
        IUnitOfWork unitOfWork;
        public AdminFlowerMngWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;
            LoadFlowerList();
        }
        private FlowerBouquet GetFlowerObj()
        {            
            if (lvFlower.SelectedIndex == -1)
            {
                throw new Exception("No item selected!");
            }
            FlowerBouquet flower = null;
            try
            {
                flower = (FlowerBouquet)lvFlower.SelectedItem;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Flower Object");
            }
            return flower;
        }
        public void LoadFlowerList()
        {
            lvFlower.ItemsSource = unitOfWork.FlowerBouquetRepository.GetAll();
            txtSearch.Text = string.Empty;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(unitOfWork);
            loginWindow.Show();
            this.Close();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customerMngWindow = new AdminCustomerMngWindow(unitOfWork);
            customerMngWindow.Show();
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
            LoadFlowerList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowerDetailWindow detailWindow = new FlowerDetailWindow(unitOfWork)
                {
                    Title = "Add Flower Bouquet",
                    InsertOrUpdate = true
                };
                detailWindow.ShowInTaskbar = false;
                detailWindow.Owner = this;
                if (detailWindow.ShowDialog().Value)
                {
                    LoadFlowerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Flower Bouquet");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowerBouquet flowerObj = GetFlowerObj();
                FlowerDetailWindow detailWindow = new FlowerDetailWindow(unitOfWork)
                {
                    Title = "Update Flower Bouquet",
                    InsertOrUpdate = false,
                    flowerOject = flowerObj
                };
                detailWindow.ShowInTaskbar = false;
                detailWindow.Owner = this;
                if (detailWindow.ShowDialog().Value)
                {
                    LoadFlowerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Flower Failed");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FlowerBouquet flowerObj = GetFlowerObj();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete the flower \"{flowerObj.FlowerBouquetName}\"?",
                    "Delete Flower Bouquet", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var detailList = unitOfWork.OrderDetailRepository
                        .GetDetailsByFlower(flowerObj.FlowerBouquetId);
                    bool flowerReference =  detailList.Count() != 0;
                    if (!flowerReference) // has NO reference in order -> delete in DB
                    {
                        unitOfWork.FlowerBouquetRepository.Delete(flowerObj);
                        MessageBox.Show($"The flower \"{flowerObj.FlowerBouquetName}\" has been deleted successfully!", "Delete Succeed");
                    }
                    else // has reference in order -> update status
                    {
                        flowerObj.FlowerBouquetStatus = 0;
                        unitOfWork.FlowerBouquetRepository.Update(flowerObj);
                        MessageBox.Show($"The status of flower \"{flowerObj.FlowerBouquetName}\" has been change to deleted", "Delete Succeed");
                    }
                    LoadFlowerList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Flower Failed");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var flowerList = unitOfWork.FlowerBouquetRepository.Search(txtSearch.Text.Trim());
            lvFlower.ItemsSource = flowerList;
        }
    }
}

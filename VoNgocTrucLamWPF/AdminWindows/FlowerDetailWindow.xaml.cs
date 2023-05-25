using BusinessObjects;
using Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Windows;
using VoNgocTrucLamWPF.Helper;

namespace VoNgocTrucLamWPF
{
    /// <summary>
    /// Interaction logic for FlowerDetailWindow.xaml
    /// </summary>
    public partial class FlowerDetailWindow : Window
    {
        IUnitOfWork unitOfWork;
        public bool InsertOrUpdate { get; set; } // true: insert, false: update
        public FlowerBouquet flowerOject { get; set; }
        public FlowerDetailWindow(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = unitOfWork;            
            // assign category data to combobox
            cboCategory.ItemsSource = unitOfWork.CategoryRepository.GetAll();
            cboCategory.DisplayMemberPath = "CategoryName";
            // assign supplier data to combobox
            cboSupplier.ItemsSource = unitOfWork.SupplierRepository.GetAll();
            cboSupplier.DisplayMemberPath = "SupplierName";
            // assign status to combobox
            cboStatus.ItemsSource = new List<string>
            {
                "Active",
                "Inactive"
            };            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // assign update information
            if (!InsertOrUpdate && flowerOject != null) //update
            {
                txtFlowerId.Text = flowerOject.FlowerBouquetId.ToString();
                cboCategory.Text = unitOfWork.CategoryRepository
                        .GetCategoryById(flowerOject.CategoryId).CategoryName;
                txtFlowerName.Text = flowerOject.FlowerBouquetName;
                txtDescription.Text = flowerOject.Description;
                txtUnitInStock.Text = flowerOject.UnitsInStock.ToString();
                txtUnitPrice.Text = flowerOject.UnitPrice.ToString();
                cboSupplier.Text = unitOfWork.SupplierRepository
                        .GetSupplierById(flowerOject.SupplierId.Value).SupplierName;
                cboStatus.Text = flowerOject.FlowerBouquetStatus == 1 ? "Active" : "Inactive";
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
                    InsertFlower();                    
                }
                else
                {
                    UpdateFlower();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save flower");
                this.DialogResult= false;
            }
        }
        private FlowerBouquet GetFlowerObject()
        {
            FlowerBouquet flower = null;
            try
            {
                string status = (string)cboStatus.SelectedItem;
                flower = new FlowerBouquet
                {
                    FlowerBouquetId = int.Parse(txtFlowerId.Text),
                    CategoryId = cboCategory.SelectedIndex != -1 ? ((Category)cboCategory.SelectedItem).CategoryId : -1,
                    FlowerBouquetName = txtFlowerName.Text,
                    Description = txtDescription.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitInStock.Text),
                    FlowerBouquetStatus = (byte)(status.Equals("Active") ? 1 : 0),
                    SupplierId = cboSupplier.SelectedIndex != -1 ? ((Supplier)cboSupplier.SelectedItem).SupplierId : -1
                };
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Flower Object");
            }
            return flower;
        }
        private void InsertFlower()
        {
            if (ChechValidate())
            {
                FlowerBouquet flowerObj = GetFlowerObject();
                unitOfWork.FlowerBouquetRepository.Insert(flowerObj);
                MessageBox.Show($"Flower \"{flowerObj.FlowerBouquetName}\" has been added successfully!", "Add Succeed");
                this.DialogResult = true;
            }
        }
        private void UpdateFlower()
        {
            if (ChechValidate())
            {
                FlowerBouquet flowerObj = GetFlowerObject();
                unitOfWork.FlowerBouquetRepository.Update(flowerObj);
                MessageBox.Show($"Flower \"{flowerOject.FlowerBouquetName}\" has been updated successfully!", "Update Succeed");
                this.DialogResult = true;
            }
        }
        private bool ChechValidate()
        {
            if (txtFlowerId.Text.Length == 0 || txtFlowerName.Text.Length == 0 
                || txtDescription.Text.Length == 0 || txtUnitInStock.Text.Length == 0
                || txtUnitPrice.Text.Length == 0 || cboCategory.SelectedIndex == -1
                || cboStatus.SelectedIndex == -1 || cboSupplier.SelectedIndex == -1)
            {
                MessageBox.Show("Input fields must not be null!", "Input Flower Info");
                return false;
            }
            if (!txtFlowerId.Text.IsNumeric())
            {
                MessageBox.Show("ID wrong format! (must be an integer)", "Input Flower Info");
                return false;
            }
            if (!txtUnitInStock.Text.IsNumeric())
            {
                MessageBox.Show("Unit In Stock wrong format! (must be an integer)", "Input Flower Info");
                return false;
            }
            if (!txtUnitPrice.Text.IsDouble())
            {
                MessageBox.Show("Unit Price wrong format! (must be an double)", "Input Flower Info");
                return false;
            }
            return true;
        }
    }
}

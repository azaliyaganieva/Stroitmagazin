using Ganieva_Azaliya_006.DBCon;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Ganieva_Azaliya_006.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddWindows.xaml
    /// </summary>
    public partial class AddWindows : Window
    {
        private string selectedPhotoPath = null;
        public AddWindows()
        {
            InitializeComponent();
            LoadComboBoxes();


        }

        private void LoadComboBoxes()
        {
            CategoryCmb.ItemsSource = Connection.build.ProductCategory.ToList();
            ManufacturerCmb.ItemsSource = Connection.build.Manufacturer.ToList();
            SupplierCmb.ItemsSource = Connection.build.Supplier.ToList();
            UnitCmb.ItemsSource = Connection.build.Unit.ToList();
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.png, *.jpeg) | *.jpg; *.png; *.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                selectedPhotoPath = openFileDialog.FileName;
                PhotoImage.Source = new BitmapImage(new Uri(selectedPhotoPath));
            }
        }

        private string SavePhoto()
        {
            if (string.IsNullOrWhiteSpace(selectedPhotoPath)) return null;

            string photosFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Photos");
            if (!Directory.Exists(photosFolder))
                Directory.CreateDirectory(photosFolder);

            string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(selectedPhotoPath);
            string destPath = System.IO.Path.Combine(photosFolder, fileName);

            File.Copy(selectedPhotoPath, destPath, true);

            return fileName;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string photoName = null;

                if (!string.IsNullOrWhiteSpace(selectedPhotoPath))
                {
                    // ПРОСТО КОПИРУЕМ В ПАПКУ С ПРОГРАММОЙ
                    string fileName = System.IO.Path.GetFileName(selectedPhotoPath);
                    string destPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    File.Copy(selectedPhotoPath, destPath, true);
                    photoName = fileName;  // Сохраняем только имя файла
                }

                Product newProduct = new Product()
                {
                    Name = NameTxb.Text,
                    Discription = DescriptionTxb.Text,
                    Price = Convert.ToDouble(PriceTxb.Text),
                    CountOnSclad = Convert.ToInt32(CountTxb.Text),
                    ActiveDiscount = Convert.ToInt32(DiscountTxb.Text),
                    IdProductCateg = (CategoryCmb.SelectedItem as ProductCategory).Id,
                    IdManufacturer = (ManufacturerCmb.SelectedItem as Manufacturer).Id,
                    IdSupplier = (SupplierCmb.SelectedItem as Supplier).Id,
                    IdUnit = (UnitCmb.SelectedItem as Unit).Id,
                    Photo = photoName
                };

                Connection.build.Product.Add(newProduct);
                Connection.build.SaveChanges();
                MessageBox.Show("Товар добавлен!", "Успех");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
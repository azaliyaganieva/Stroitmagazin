using Ganieva_Azaliya_006.DBCon;
using Ganieva_Azaliya_006.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ganieva_Azaliya_006.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminProductList.xaml
    /// </summary>
    public partial class AdminProductList : Page
    { public static List<Product> products { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public static Supplier currentSupplier = null;
        public static string searchText = "";
        public static string sortCurrent = "Все поставщики";
        public AdminProductList(User user)
        {
            InitializeComponent();
            products = new List<Product>(Connection.build.Product.ToList());
            suppliers=new List<Supplier>(Connection.build.Supplier.ToList());
            suppliers.Insert(0, new Supplier { Id = -1, Name = "Все поставщики" });
            
            this.DataContext = this;
            FIOTb.Text = $"Admin:{user.FIO}";
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            
        }

        private void SortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = SortCmb.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                sortCurrent=selectedItem.Content.ToString();
            }
            ApplyFilter();
        }

        private void SupplierCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           currentSupplier=SupplierCmb.SelectedItem as Supplier;
            ApplyFilter();

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchText=SearchTb.Text;
            ApplyFilter();
           
        }

        private void ApplyFilter()
        {
            List<Product> filtreprod = products;
            //Фильтр по поставщикам
            if (currentSupplier != null & currentSupplier.Id != -1)
            {
                filtreprod = filtreprod.Where(x => x.IdSupplier == currentSupplier.Id).ToList();
            }
            else
                filtreprod = products;

            //Поиск

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filtredprod = filtredprod.Where(x => x.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                 x.ProductCategory.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                 x.Discription.ToLower().Contains(SearchTb.Text.ToLower()) ||
                 x.Manufacturer.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                  x.Supplier.Name.ToLower().Contains(SearchTb.Text.ToLower())).ToList();
            }

            //Сортировка
            if (sortCurrent == "По возрастанию")
            {
                filtreprod = filtreprod.OrderBy(x => x.CountOnSclad).ToList();

            }
            else if (sortCurrent == "По убыванию")
            {
                filtreprod = filtreprod.OrderByDescending(x => x.CountOnSclad).ToList();

            }
            else
                filtreprod = filtreprod;
            productLv.ItemsSource=filtreprod;


            
        }

        private void productLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindows addWindows = new AddWindows();
            addWindows.ShowDialog();
            products = Connection.build.Product.ToList();
            productLv.ItemsSource = products;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using Ganieva_Azaliya_006.DBCon;
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
    /// Логика взаимодействия для ManagerProductList.xaml
    /// </summary>
    public partial class ManagerProductList : Page
    {public static List<Product> products { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public static Supplier currentSupplier = null;
        public static string searchText = "";
        public static string sortCurrent = "Все поставщики";
        public ManagerProductList(User user)
        {
            InitializeComponent();
            products = new List<Product>(Connection.build.Product.ToList());
            suppliers = new List<Supplier>(Connection.build.Supplier.ToList());
            suppliers.Insert(0, new Supplier { Id = -1, Name = "Все поставщики" });

            this.DataContext = this;
            FIOTb.Text = $"Менеджер:{user.FIO}";
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
                sortCurrent = selectedItem.Content.ToString();
            }
            ApplyFilter();
        }

        private void SupplierCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSupplier = SupplierCmb.SelectedItem as Supplier;
            ApplyFilter();

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchText = SearchTb.Text;
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            List<Product> filtredprod = products;
            //Фильтр по поставщикам
            if (currentSupplier != null && currentSupplier.Id != -1)
            {
                filtredprod = filtredprod.Where(x => x.IdSupplier == currentSupplier.Id).ToList();
            }
            else
                filtredprod = products;

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
                filtredprod = filtredprod.OrderBy(x => x.CountOnSclad).ToList();
            }
            else if (sortCurrent == "По убыванию")
            {
                filtredprod = filtredprod.OrderByDescending(x => x.CountOnSclad).ToList();
            }
            else
                filtredprod = filtredprod;
            productLv.ItemsSource = filtredprod;
        }

        private void productLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}

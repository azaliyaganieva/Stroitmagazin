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
    /// Логика взаимодействия для ClientProductList.xaml
    /// </summary>
    public partial class ClientProductList : Page
    {
        public static List<Product> products { get; set; }
        public ClientProductList(User user)
        {
            InitializeComponent();
            products = new List<Product>(Connection.build.Product.ToList());
            this.DataContext = this;
            FIOTb.Text = $"пользователь:{user.FIO}";
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void productLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

using Ganieva_Azaliya_006.DBCon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для AuthPages.xaml
    /// </summary>
    public partial class AuthPages : Page
    {
        public static List<User> users   {get; set; }
        public AuthPages()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string password=PassPb.Password.Trim();
            users=new List<User>(Connection.build.User.ToList());
            User currentUser=users.FirstOrDefault(x=>x.Login.Trim()==login&&x.Password.Trim()==password);
            if(currentUser!=null)
            {
                MessageBox.Show("авторизация прошла успешно");
                if (currentUser.Id == 1)
                {
                    NavigationService.Navigate(new AdminProductList(currentUser));

                }
                if (currentUser.Id == 2)
                {
                    NavigationService.Navigate(new ManagerProductList(currentUser));

                }
                if (currentUser.Id == 3)
                {
                    NavigationService.Navigate(new ClientProductList(currentUser));

                }
            }
        }

        private void GuestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuestProductList());
        }
    }
}

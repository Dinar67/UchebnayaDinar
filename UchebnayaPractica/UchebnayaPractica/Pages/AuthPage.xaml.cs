using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void RegLink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = App.db.User.FirstOrDefault(x => x.Login == LoginTb.Text && x.Password == PasswordPb.Password);
            if (user != null)
            {
                if(RememberCb.IsChecked == true)
                    File.WriteAllText(@"RememberMe.txt", user.Login);
                App.currentUser = user;

                if (user.RoleId == 3)
                {
                    App.mainWindow.SetIcons(true, true, true, true, true, true);
                    NavigationService.Navigate(new EmployeePage());
                }
                else if (user.RoleId == 4)
                {
                    App.mainWindow.SetIcons(false, false, true, true, false, true);
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    App.mainWindow.SetIcons(false, true, true, true, false, true);
                    NavigationService.Navigate(new AccessoriesAndMaterialsPage());
                }
                
                Methods.TakeInformation("Вы успешно зашли в систему!");
            }
            else
                Methods.TakeWarning("Неверный логин или пароль!");
        }
    }
}

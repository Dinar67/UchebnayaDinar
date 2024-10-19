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
                NavigationService.Navigate(new EmployeePage());
                Methods.TakeInformation("Вы успешно зашли в систему!");
                App.mainWindow.Exit.Visibility = Visibility.Visible;
                App.mainWindow.Person.Visibility = Visibility.Visible;
            }
            else
                Methods.TakeInformation("Неверный логин или пароль!");
        }
    }
}

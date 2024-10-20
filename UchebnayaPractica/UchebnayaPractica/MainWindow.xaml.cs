using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using UchebnayaPractica.Database;
using UchebnayaPractica.Pages;

namespace UchebnayaPractica
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.mainWindow = this;
            if (File.Exists(@"RememberMe.txt"))
            {
                string Login = File.ReadAllText(@"RememberMe.txt");
                App.currentUser = App.db.User.FirstOrDefault(x => x.Login == Login);
                if(App.currentUser == null)
                {
                    MainFrame.Navigate(new AuthPage());
                    return;
                }
                if (App.currentUser.RoleId == 3)
                {
                    App.mainWindow.SetIcons(true, true, true, true);
                    MainFrame.Navigate(new EmployeePage());
                }
                else if (App.currentUser.RoleId == 4)
                {
                    App.mainWindow.SetIcons(false, false, true, true);
                    MainFrame.Navigate(new MainPage());
                }
                else
                {
                    App.mainWindow.SetIcons(false, true, true, true);
                    MainFrame.Navigate(new AccessoriesAndMaterialsPage());
                }
                Methods.TakeInformation("Вы успешно зашли в систему!");
            }
            else
                MainFrame.Navigate(new AuthPage());
        }

        public void SetIcons(bool employee, bool materials, bool exit, bool account)
        {
            Employee.Visibility = employee ? Visibility.Visible : Visibility.Collapsed;
            Material.Visibility = materials ? Visibility.Visible : Visibility.Collapsed;
            Person.Visibility = account ? Visibility.Visible : Visibility.Collapsed;
            Exit.Visibility = exit ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Exit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (File.Exists(@"RememberMe.txt"))
                File.Delete(@"RememberMe.txt");
            MainFrame.Navigate(new AuthPage());
            SetIcons(false, false, false, false);
        }

        private void Material_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new AccessoriesAndMaterialsPage());
        }

        private void Employee_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new EmployeePage());
        }

        private void Person_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new AddEditEmployee(App.currentUser, false, "Ваш профиль"));
        }
    }
}

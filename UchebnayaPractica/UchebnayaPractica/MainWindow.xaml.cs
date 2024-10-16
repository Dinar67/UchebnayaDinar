﻿using System.IO;
using System.Linq;
using System.Windows;
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
                MainFrame.Navigate(new MainPage());
                Exit.Visibility = Visibility.Visible;
                Person.Visibility = Visibility.Visible;
                Methods.TakeInformation("Вы успешно зашли в систему!");
            }
            else
                MainFrame.Navigate(new AuthPage());
        }
        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Exit_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (File.Exists(@"RememberMe.txt"))
            {
                File.Delete(@"RememberMe.txt");
                MainFrame.Navigate(new AuthPage());
                Exit.Visibility = Visibility.Collapsed;
                Person.Visibility = Visibility.Collapsed;
            }
        }
    }
}

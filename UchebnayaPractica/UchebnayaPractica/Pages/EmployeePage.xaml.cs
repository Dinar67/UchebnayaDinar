using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
            SortCb.SelectedIndex = 0;
        }

        public void Refresh()
        {
            IEnumerable<User> employee = App.db.User.Where(x => x.RoleId != 4);

            if(SearchTb.Text != "")
                employee = employee.Where(x => x.FIO.Contains(SearchTb.Text));


            if (SortCb.SelectedIndex == 1)
                employee = employee.OrderBy(x => x.LastName);
            else if (SortCb.SelectedIndex == 2)
                employee = employee.OrderByDescending(x => x.LastName);
            else if (SortCb.SelectedIndex == 3)
                employee = employee.OrderBy(x => x.Age);
            else if (SortCb.SelectedIndex == 4)
                employee = employee.OrderByDescending(x => x.Age);
            else if (SortCb.SelectedIndex == 5)
                employee = employee.OrderBy(x => x.RoleId);


            MyList.ItemsSource = employee.ToList();
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditEmployee(new User(), true));
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить сотрудника?"))
                Methods.TakeInformation("Успешно удалено!");
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditEmployee((sender as Image).DataContext as User,false, "Редактировать сотрудника"));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using UchebnayaPractica.Database;
using UchebnayaPractica.Windows;
using System.Windows;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class FailurePage : Page
    {
        public FailurePage()
        {
            InitializeComponent();
            SortCb.SelectedIndex = 0;
        }

        public void Refresh()
        {
            IEnumerable<HardwareFailure> failures = App.db.HardwareFailure;

            if (SearchTb.Text != "")
                failures = failures.Where(x => x.Equipment.Model.Contains(SearchTb.Text) 
                || x.FailureStart.ToString().Contains(SearchTb.Text)
                || x.FailureEnd.ToString().Contains(SearchTb.Text));

            if (SortCb.SelectedIndex == 1)
                failures = failures.OrderBy(x => x.FailureStart);
            else if (SortCb.SelectedIndex == 2)
                failures = failures.OrderByDescending(x => x.FailureStart);
            else if (SortCb.SelectedIndex == 3)
                failures = failures.OrderBy(x => x.FailureEnd);
            else if (SortCb.SelectedIndex == 4)
                failures = failures.OrderByDescending(x => x.FailureEnd);
            else if (SortCb.SelectedIndex == 5)
                failures = failures.OrderBy(x => x.Equipment.Model);

            MyList.ItemsSource = failures.ToList();
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить поломку?"))
            {
                App.db.HardwareFailure.Remove((sender as Image).DataContext as HardwareFailure);
                Refresh();
                Methods.TakeInformation("Успешно удалено!");
            }
        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditFailure((sender as Image).DataContext as HardwareFailure, this));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void AddOrderBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditFailure(new HardwareFailure(), this));
        }
    }
}

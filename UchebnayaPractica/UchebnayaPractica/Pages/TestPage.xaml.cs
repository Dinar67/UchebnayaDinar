using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;
using System;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public TestPage()
        {
            InitializeComponent();
            SortCb.SelectedIndex = 0;
        }

        public void Refresh()
        {
            IEnumerable<Product> products = App.db.Product.Where(x => x.Test.Count() > 0 && x.Order.Count() > 0);

            if (SearchTb.Text != "")
                products = products.Where(x => x.Name.Contains(SearchTb.Text)
                || x.Order.First().OrderNumber.Contains(SearchTb.Text));

            if (SortCb.SelectedIndex == 1)
                products = products.OrderBy(x => x.Name);
            else if (SortCb.SelectedIndex == 2)
                products = products.OrderByDescending(x => x.Name);
            else if (SortCb.SelectedIndex == 3)
                products = products.OrderBy(x => x.Passed.Text);

            MyList.ItemsSource = products.ToList();
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (!Methods.TakeChoice("Вы точно хотите удалить тест продукта?"))
                    return;
                List<Test> tests = ((sender as Image).DataContext as Product).Test.ToList();
                foreach (var test in tests)
                    App.db.Test.Remove(test);
                App.db.SaveChanges();
                Refresh();
                Methods.TakeInformation("Тест успешно удален!");
            }
            catch (Exception ex) { Methods.TakeWarning("Невозможео удалить тесты!\n" + ex.Message); }

        }

        private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditTestPage((sender as Image).DataContext as Product));
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
            NavigationService.Navigate(new AddEditTestPage(new Product()));
        }
    }
}

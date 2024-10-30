using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;
using UchebnayaPractica.Pages;

namespace UchebnayaPractica.Components
{
    /// <summary>
    /// Логика взаимодействия для MaterialControl.xaml
    /// </summary>
    public partial class TestControl : UserControl
    {
        public Test test;
        AddEditTestPage page;
        public TestControl(Test test, AddEditTestPage page)
        {
            InitializeComponent();
            this.page = page;
            this.test = test;

            if(test.Id == 0)
                PassedCb.IsChecked = true;
            if (test.isPassed != null && test.isPassed == false)
            {
                PassedCb.IsChecked = false;
                ReasonTb.Visibility = System.Windows.Visibility.Visible;
            }
            else if(test.isPassed != null && test.isPassed == true)
            {
                PassedCb.IsChecked = true;
                ReasonTb.Visibility = System.Windows.Visibility.Collapsed;
            }

            DataContext = test;
        }

        private void Trash_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить критерий?"))
            {
                page.tests.Remove(this);
                if (test.Id != 0)
                    App.db.Test.Remove(test);
                page.Refresh();
                Methods.TakeInformation("Критерий успешно удален!");
            }
        }

        private void PassedCb_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ReasonTb.Visibility = System.Windows.Visibility.Collapsed;
            test.isPassed = PassedCb.IsChecked;
        }

        private void PassedCb_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ReasonTb.Visibility = System.Windows.Visibility.Visible;
            test.isPassed = PassedCb.IsChecked;
        }

        private void ReasonTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            test.Description = ReasonTb.Text;
        }
    }
}

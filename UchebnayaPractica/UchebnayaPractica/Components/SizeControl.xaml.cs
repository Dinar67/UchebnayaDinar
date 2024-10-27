using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Components
{
    /// <summary>
    /// Логика взаимодействия для MaterialControl.xaml
    /// </summary>
    public partial class SizeControl : UserControl
    {
        public Size size;

        ProductControl productControl;
        public SizeControl(Size size, ProductControl productControl)
        {
            InitializeComponent();
            this.size = size;
            this.productControl = productControl;
            UnitCb.ItemsSource = App.db.Unit.ToList();
            DataContext = size;
        }
        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Trash_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить размер?"))
            {
                productControl.sizes.Remove(this);
                if(size.Id != 0)
                {
                    App.db.Size.Remove(size);
                    App.db.SaveChanges();
                }             
                productControl.RefreshSizes();
                Methods.TakeInformation("Размер успешно удален!");
            }
        }

        private void NameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            size.Name = NameTb.Text;
        }

        private void CountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            size.SizeValue = Convert.ToInt32(CountTb.Text);
        }

        private void UnitCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            size.IdUnit = (UnitCb.SelectedItem as Unit).Id;
        }
    }
}

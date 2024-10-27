using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Components
{
    /// <summary>
    /// Логика взаимодействия для MaterialControl.xaml
    /// </summary>
    public partial class AccessoriesControl : UserControl
    {
        public ProductAccessories accessories;


        ProductControl productControl;
        public AccessoriesControl(ProductAccessories accessories, ProductControl productControl)
        {
            InitializeComponent();
            this.accessories = accessories;
            this.productControl = productControl;
            MaterialCb.ItemsSource = App.db.Accessories.ToList();
            DataContext = accessories;
        }


        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void MaterialCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            accessories.AccessoriesArticle = (MaterialCb.SelectedItem as Accessories).Article;
        }

        private void Trash_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить комплектующее?"))
            {
                productControl.accessories.Remove(this);
                if (accessories.Id != 0)
                {
                    App.db.ProductAccessories.Remove(accessories);
                    App.db.SaveChanges();
                }  
                productControl.RefreshAccessories();
                Methods.TakeInformation("Комплектующее успешно удалено!");
            }
        }

        private void CountTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            accessories.Count = Convert.ToInt32(CountTb.Text);
        }
    }
}

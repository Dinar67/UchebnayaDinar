using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccessoriesAndMaterialsPage.xaml
    /// </summary>
    public partial class AccessoriesAndMaterialsPage : Page
    {
        public AccessoriesAndMaterialsPage()
        {
            InitializeComponent();
            RefreshMaterial();
            RefreshAccessories();
        }

        public void RefreshMaterial()
        {
            IEnumerable<Material> materials = App.db.Material;
            MaterialsListView.ItemsSource = materials.ToList();
        }
        public void RefreshAccessories()
        {
            IEnumerable<Accessories> accessories = App.db.Accessories;
            ComponentsListView.ItemsSource = accessories.ToList();
        }

        private void EditMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditAccessories_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeleteAccessories_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

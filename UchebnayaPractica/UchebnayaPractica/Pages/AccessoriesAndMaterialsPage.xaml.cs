using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;
using Material = UchebnayaPractica.Database.Material;

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
            MaterialSortCb.SelectedIndex = 0;
            AccessoriesSortCb.SelectedIndex = 0;
            List<Sklad> sklads = App.db.Sklad.ToList().Concat(new List<Sklad> { new Sklad() { Name = "Все" } }).ToList();
            MaterialFilterCb.ItemsSource = sklads;
            AccessoriesFilterCb.ItemsSource = sklads;

            if (App.currentUser.RoleId == 3 || App.currentUser.RoleId == 5)
            {
                AddAccessoriesBtn.Visibility = Visibility.Visible;
                AddMaterialBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AddAccessoriesBtn.Visibility = Visibility.Collapsed;
                AddMaterialBtn.Visibility = Visibility.Collapsed;
            }
        }

        public void RefreshMaterial()
        {
            IEnumerable<Material> materials = App.db.Material;
            int count = materials.Count();
            
            //Поиск
            if (MaterialSearchTb.Text != "")
                materials = materials.Where(x => x.Article.ToLower().Contains(MaterialSearchTb.Text.ToLower()) || x.Name.ToLower().Contains(MaterialSearchTb.Text.ToLower()));

            //Сортировка
            if (MaterialSortCb.SelectedIndex == 1)
                materials = materials.OrderBy(x => x.Article);
            else if (MaterialSortCb.SelectedIndex == 2)
                materials = materials.OrderBy(x => x.Name);
            else if (MaterialSortCb.SelectedIndex == 3)
                materials = materials.OrderByDescending(x => x.Name);
            else if (MaterialSortCb.SelectedIndex == 4)
                materials = materials.OrderBy(x => x.Count);
            else if (MaterialSortCb.SelectedIndex == 5)
                materials = materials.OrderByDescending(x => x.Count);
            else if (MaterialSortCb.SelectedIndex == 6)
                materials = materials.OrderBy(x => x.SupplierName);

            //Фильтр по складам
            if(MaterialFilterCb.SelectedIndex != -1 && MaterialFilterCb.SelectedIndex != App.db.Sklad.Count())
                materials = materials.Where(x => x.IdSklad == (MaterialFilterCb.SelectedItem as Sklad).Id);

            MaterialsListView.ItemsSource = materials.ToList();
            MaterialCountTb.Text = $"{materials.Count()} из {count}";
            decimal price = 0;
            foreach (var material in materials)
                price += (material.PriceOneKg == null? 0 : (decimal)material.PriceOneKg);
            MaterialPriceTb.Text = $"{price}";
        }
        public void RefreshAccessories()
        {
            IEnumerable<Accessories> accessories = App.db.Accessories;
            int count = accessories.Count();

            //Поиск
            if (AccessoriesSearchTb.Text != "")
                accessories = accessories.Where(x => x.Article.ToLower().Contains(AccessoriesSearchTb.Text.ToLower()) || x.Name.ToLower().Contains(AccessoriesSearchTb.Text.ToLower()));

            //Сортировка
            if (AccessoriesSortCb.SelectedIndex == 1)
                accessories = accessories.OrderBy(x => x.Article);
            else if (AccessoriesSortCb.SelectedIndex == 2)
                accessories = accessories.OrderBy(x => x.Name);
            else if (AccessoriesSortCb.SelectedIndex == 3)
                accessories = accessories.OrderByDescending(x => x.Name);
            else if (AccessoriesSortCb.SelectedIndex == 4)
                accessories = accessories.OrderBy(x => x.Count);
            else if (AccessoriesSortCb.SelectedIndex == 5)
                accessories = accessories.OrderByDescending(x => x.Count);
            else if (AccessoriesSortCb.SelectedIndex == 6)
                accessories = accessories.OrderBy(x => x.SupplierName);

            //Фильтр по складам
            if (AccessoriesFilterCb.SelectedIndex != -1 && AccessoriesFilterCb.SelectedIndex != App.db.Sklad.Count())
                accessories = accessories.Where(x => x.IdSklad == (AccessoriesFilterCb.SelectedItem as Sklad).Id);

            ComponentsListView.ItemsSource = accessories.ToList();
            AccessoriesCountTb.Text = $"{accessories.Count()} из {count}";
            decimal price = 0;
            foreach (var material in accessories)
                price += (material.Price == null ? 0 : (decimal)material.Price);
            AccessoriesPriceTb.Text = $"{price}";
        }

        private void EditMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditMaterialPage((sender as Image).DataContext as Material));
        }

        private void EditAccessories_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccessoriesPage((sender as Image).DataContext as Accessories));
        }

        private void DeleteMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Material material = (sender as Image).DataContext as Material;
            if (Methods.TakeChoice("Вы точно хотите удалить материал?"))
            {
                if(material.Count != 0)
                {
                    Methods.TakeWarning("Вы не можете удалить этот материал, на складе есть остатки материала!");
                    return;
                }
                App.db.Material.Remove(material);
                App.db.SaveChanges();
                RefreshMaterial();
                Methods.TakeInformation("Успешно удалено");
            }
        }

        private void DeleteAccessories_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Accessories accessories = (sender as Image).DataContext as Accessories;
            if (Methods.TakeChoice("Вы точно хотите удалить комлпектующее?"))
            {
                if (accessories.Count != 0)
                {
                    Methods.TakeWarning("Вы не можете удалить это комплектующее, на складе есть остатки комплектующих!");
                    return;
                }
                App.db.Accessories.Remove(accessories);
                App.db.SaveChanges();
                RefreshAccessories();
                Methods.TakeInformation("Успешно удалено");
            }
        }

        private void MaterialSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshMaterial();
        }
        private void MaterialSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshMaterial();
        }
        private void MaterialFilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshMaterial();
        }


        private void AccessoriesSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshAccessories();
        }
        private void AccessoriesSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAccessories();
        }
        private void AccessoriesFilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAccessories();
        }

        private void AddMaterialBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditMaterialPage(new Material()));
        }

        private void AddAccessoriesBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditAccessoriesPage(new Accessories()));
        }
    }
}

using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditMaterialPage.xaml
    /// </summary>
    public partial class AddEditMaterialPage : Page
    {
        Material material;
        MaterialImage materialImage;
        bool isNew;
        string oldArticle;
        public AddEditMaterialPage(Material material)
        {
            InitializeComponent();
            this.material = material;
            oldArticle = material.Article;
            if (material.Article == null) isNew = true;
            materialImage = material.MaterialImage;
            UnitCb.ItemsSource = App.db.Unit.ToList();
            SupplierCb.ItemsSource = App.db.Supplier.ToList();
            MaterialTypeCb.ItemsSource = App.db.TypeMaterial.ToList();
            StandartCb.ItemsSource = App.db.Standart.ToList();
            SkladCb.ItemsSource = App.db.Sklad.ToList();

            if (material.Article != null)
            {
                TitleTb.Text = "Редактировать материал";
                if (materialImage != null)
                    MainImage.Source = Methods.GetBitmapImageFromBytes(materialImage.Photo);
                UnitCb.SelectedItem = material.Unit;
                SupplierCb.SelectedItem = material.Supplier;
                MaterialTypeCb.SelectedItem = material.TypeMaterial;
                StandartCb.SelectedItem = material.Standart;
                SkladCb.SelectedItem = material.Sklad;
            }

            DataContext = material;
        }

        private void Back_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void LoadBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var opn = new OpenFileDialog();
            opn.Title = "Выберите изображение";
            opn.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files|*.*";
            if (opn.ShowDialog() == true)
            {
                byte[] bytes = File.ReadAllBytes(opn.FileName);
                MaterialImage image = App.db.MaterialImage.FirstOrDefault(x => x.Photo == bytes);
                if (image != null)
                {
                    material.IdMaterialImage = image.Id;
                    materialImage = image;
                }
                else
                    materialImage = new MaterialImage() { Photo = bytes };

                MainImage.Source = Methods.GetBitmapImageFromBytes(bytes);
            }
        }

        private void Delete_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainImage.Source = null;
            material.IdMaterialImage = null;

            if (materialImage.Id == 0)
                materialImage = null;
            else
            {
                if (isNew && materialImage.Material.Count() > 0)
                    material.IdMaterialImage = null;
                else if (!isNew && materialImage.Material.Count() > 1)
                    material.IdMaterialImage = null;
                else if(!isNew && materialImage.Material.Count() == 1)
                {
                    material.IdMaterialImage = null;
                    App.db.MaterialImage.Remove(materialImage);
                }                  
            }
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string mistake = "";

            if (ArticleTb.Text == "" && mistake == "")
                mistake = "Вы не заполнили артикуль!";
            if (App.db.Material.Any(x => x.Article == ArticleTb.Text) && (oldArticle != ArticleTb.Text || isNew) && mistake == "")
                mistake = "Такой артикуль уже есть!";
            if (NameTb.Text == "" && mistake == "")
                mistake = "Вы не заполнили наименование!";
            if (SupplierCb.SelectedIndex == -1 && mistake == "")
                mistake = "Вы не выбрали поставщика!";
            if (SkladCb.Text == "" && mistake == "")
                mistake = "Вы не выбрали склад!";        

            if (mistake != "")
            {
                Methods.TakeWarning(mistake);
                return;
            }
            
            material.SupplierName = (SupplierCb.SelectedItem as Supplier).SupplierName;
            material.IdSklad = (SkladCb.SelectedItem as Sklad).Id;
            if (MaterialTypeCb.SelectedIndex != -1)
                material.IdTypeMaterial = (MaterialTypeCb.SelectedItem as TypeMaterial).Id;
            if (StandartCb.SelectedIndex != -1)
                material.IdStandart = (StandartCb.SelectedItem as Standart).Id;
            if (UnitCb.SelectedIndex != -1)
                material.IdUnit = (UnitCb.SelectedItem as Unit).Id;

            if (materialImage != null && materialImage.Id == 0)
            {
                materialImage = App.db.MaterialImage.Add(materialImage);
                material.IdMaterialImage = materialImage.Id;
            }

            if (isNew)
                App.db.Material.Add(material);

            App.db.SaveChanges();
            NavigationService.Navigate(new AccessoriesAndMaterialsPage());
            Methods.TakeInformation("Изменения успешно сохранены!");
        }
    }
}

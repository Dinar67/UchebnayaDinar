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
    public partial class AddEditAccessoriesPage : Page
    {
        Accessories accessories;
        AcessoriesImage accessoriesImage;
        bool isNew;
        string oldArticle;
        public AddEditAccessoriesPage(Accessories accessories)
        {
            InitializeComponent();
            this.accessories = accessories;
            oldArticle = accessories.Article;
            if (accessories.Article == null) isNew = true;
            accessoriesImage = accessories.AcessoriesImage;
            UnitCb.ItemsSource = App.db.Unit.ToList();
            SupplierCb.ItemsSource = App.db.Supplier.ToList();
            MaterialTypeCb.ItemsSource = App.db.TypeAccessories.ToList();
            SkladCb.ItemsSource = App.db.Sklad.ToList();

            if (accessories.Article != null)
            {
                TitleTb.Text = "Редактировать комплектующиее";
                if (accessoriesImage != null)
                    MainImage.Source = Methods.GetBitmapImageFromBytes(accessoriesImage.Photo);
                UnitCb.SelectedItem = accessories.Unit;
                SupplierCb.SelectedItem = accessories.Supplier;
                MaterialTypeCb.SelectedItem = accessories.TypeAccessories;
                SkladCb.SelectedItem = accessories.Sklad;
            }

            DataContext = accessories;
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
                AcessoriesImage image = App.db.AcessoriesImage.FirstOrDefault(x => x.Photo == bytes);
                if (image != null)
                {
                    accessories.IdAcessoriesImage = image.Id;
                    accessoriesImage = image;
                }
                else
                    accessoriesImage = new AcessoriesImage() { Photo = bytes };

                MainImage.Source = Methods.GetBitmapImageFromBytes(bytes);
            }
        }

        private void Delete_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainImage.Source = null;
            accessories.IdAcessoriesImage = null;

            if (accessoriesImage.Id == 0)
                accessoriesImage = null;
            else
            {
                if (isNew && accessoriesImage.Accessories.Count() > 0)
                    accessories.IdAcessoriesImage = null;
                else if (!isNew && accessoriesImage.Accessories.Count() > 1)
                    accessories.IdAcessoriesImage = null;
                else if(!isNew && accessoriesImage.Accessories.Count() == 1)
                {
                    accessories.IdAcessoriesImage = null;
                    App.db.AcessoriesImage.Remove(accessoriesImage);
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

            accessories.SupplierName = (SupplierCb.SelectedItem as Supplier).SupplierName;
            accessories.IdSklad = (SkladCb.SelectedItem as Sklad).Id;
            if (MaterialTypeCb.SelectedIndex != -1)
                accessories.IdTypeAccessories = (MaterialTypeCb.SelectedItem as TypeAccessories).Id;
            if (UnitCb.SelectedIndex != -1)
                accessories.IdUnit = (UnitCb.SelectedItem as Unit).Id;

            if (accessoriesImage != null && accessoriesImage.Id == 0)
            {
                accessoriesImage = App.db.AcessoriesImage.Add(accessoriesImage);
                accessories.IdAcessoriesImage = accessoriesImage.Id;
            }

            if (isNew)
                App.db.Accessories.Add(accessories);

            App.db.SaveChanges();
            NavigationService.Navigate(new AccessoriesAndMaterialsPage());
            Methods.TakeInformation("Изменения успешно сохранены!");
        }
    }
}

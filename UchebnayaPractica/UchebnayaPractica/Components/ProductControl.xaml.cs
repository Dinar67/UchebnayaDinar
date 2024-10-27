using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;
using System.Windows;
using Size = UchebnayaPractica.Database.Size;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace UchebnayaPractica.Components
{
    /// <summary>
    /// Логика взаимодействия для ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        public Product product;
        public ProductDetail detail;

        public List<ProductControl> products = new List<ProductControl>();
        public List<MaterialControl> materials = new List<MaterialControl>();
        public List<AccessoriesControl> accessories = new List<AccessoriesControl>();
        public List<OperationControl> operations = new List<OperationControl>();
        public List<SizeControl> sizes = new List<SizeControl>();

        ProductControl productControl;
        bool isMain;
        bool isNew;
        public ProductControl(bool isMain, Product product, ProductControl productControl, ProductDetail detail)
        {
            InitializeComponent();
            this.isMain = isMain;
            this.detail = detail;
            if (isMain)
            {
                CountTb.Visibility = Visibility.Collapsed;
                MyControl.MinWidth = 975d;
                Trash.Visibility = Visibility.Collapsed;
            }
            else
                MyControl.MinWidth = productControl.MinWidth - 15d;
            this.product = product;
            this.productControl = productControl;
            if (product.Id != 0)
            {
                LoadData();
                RefreshProducts();
                RefreshMaterals();
                RefreshAccessories();
                RefreshOperations();
                RefreshSizes();
            }
            DataContext = product;
            CountTb.DataContext = detail;
        }

        private void LoadData()
        {
            foreach (var proDet in product.ProductDetail)
                products.Add(new ProductControl(false, proDet.Product1, this, proDet));

            foreach (var mat in product.ProductMaterial)
                materials.Add(new MaterialControl(mat,this));

            foreach (var acc in product.ProductAccessories)
                accessories.Add(new AccessoriesControl(acc, this));

            foreach (var ope in product.OperationSpecification)
                operations.Add(new OperationControl(ope, this));

            foreach (var size in product.Size)
                sizes.Add(new SizeControl(size, this));
        }
        public bool CheckData()
        {
            if (NameTb.Text == "")
                return false;
            if (!isMain && CountTb.Text == "")
                return false;

            foreach (var product in products)
            {
                if (!product.CheckData())
                    return false;
            }

            return true;
        }
        public void SaveProduct()
        {
            //add product
            if(product.Id == 0)
                App.db.Product.Add(product);
            App.db.SaveChanges();
            //add material 
            foreach (var material in materials)
            {
                if(material.material.MaterialArticle != null && material.material.Id == 0)
                {
                    material.material.IdProduct = product.Id;
                    App.db.ProductMaterial.Add(material.material);
                }
            }

            //add accessories 
            foreach (var accessories in accessories)
            {
                if (accessories.accessories.AccessoriesArticle != null && accessories.accessories.Id == 0)
                {
                    accessories.accessories.IdProduct = product.Id;
                    App.db.ProductAccessories.Add(accessories.accessories);
                }
            }

            //add Size
            foreach (var size in sizes)
            {
                if (size.size.Name != "" && size.size.IdUnit != null && size.size.SizeValue != null && size.size.Id == 0)
                {
                    size.size.IdProduct = product.Id;
                    App.db.Size.Add(size.size);
                }
            }

            //add Operation
            foreach (var operation in operations)
            {
                if (operation.operation.Operation != "" && operation.TypeEquipmentCb.SelectedIndex != -1 && operation.operation.IdProduct == 0)
                {
                    operation.operation.IdProduct = product.Id;
                    App.db.OperationSpecification.Add(operation.operation);
                }
            }

            foreach (var product in products)
                product.SaveProduct();
        }
        public void SaveHierarchy()
        {
            foreach (var product in products)
            {
                if(product.detail == null)
                {
                    App.db.ProductDetail.Add(new ProductDetail()
                    {
                        Count = Convert.ToInt32(product.CountTb.Text),
                        IdDetail = product.product.Id,
                        IdProduct = this.product.Id
                    });
                }
                product.SaveHierarchy();
            }
        }
        public void RefreshMaterals() 
        {
            MaterialWrap.Children.Clear();
            foreach (var material in materials)
                MaterialWrap.Children.Add(material);
        }
        public void RefreshAccessories() 
        {
            AccessoriesWrap.Children.Clear();
            foreach (var accessories in accessories)
                AccessoriesWrap.Children.Add(accessories);
        }
        public void RefreshProducts() 
        {
            ProductWrap.Children.Clear();
            foreach (var product in products)
                ProductWrap.Children.Add(product);
        }
        public void RefreshOperations() 
        {
            OperationWrap.Children.Clear();
            operations = operations.OrderBy(x => x.operation.Number).ToList();
            foreach (var operation in operations)
                OperationWrap.Children.Add(operation);
        }
        public void RefreshSizes()
        {
            SizeWrap.Children.Clear();
            foreach (var size in sizes)
                SizeWrap.Children.Add(size);
        }

        private void Trash_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить изделие?"))
            {
                DeleteProduct();
                Methods.TakeInformation("Изделие успешно удалено!");
            }
        }

        public void DeleteProduct()
        {
            productControl.products.Remove(this);
            if (product.Id != 0)
            {
                if (detail != null)
                    App.db.ProductDetail.Remove(detail);

                foreach(var pro in products)
                {
                    App.db.ProductDetail.Remove(pro.detail);
                    pro.DeleteProduct();
                }

                App.db.Product.Remove(product);
                App.db.SaveChanges();
            }
            productControl.RefreshProducts();
        }

        private void AddProductBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            products.Add(new ProductControl(false, new Product(), this, null));
            RefreshProducts();
        }

        private void AddMaterialBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            materials.Add(new MaterialControl(new ProductMaterial(), this));
            RefreshMaterals();
        }

        private void AddAccessoriesBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            accessories.Add(new AccessoriesControl(new ProductAccessories(), this));
            RefreshAccessories();
        }

        private void AddOperationBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int number = 0;
            if (number == 0 && operations.Count() == 0)
                number = 1;

            if (number == 0 && operations.Count() == operations[operations.Count() - 1].operation.Number)
                number = operations.Count() + 1;

            if(number == 0)
            {
                for (int i = 1; i <= operations.Count(); i++)
                {
                    if (number != 0 || i == operations[i - 1].operation.Number)
                        continue;
                    else
                        number = i;
                }
            }
            operations.Add(new OperationControl(new OperationSpecification() { Number = number }, this));
            RefreshOperations();
        }

        private void AddSizeBtn_Click(object sender, RoutedEventArgs e)
        {
            sizes.Add(new SizeControl(new Size(), this));
            RefreshSizes();
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

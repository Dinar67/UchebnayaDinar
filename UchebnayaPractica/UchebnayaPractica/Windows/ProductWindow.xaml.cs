using System;
using System.Windows;
using UchebnayaPractica.Components;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        Order order;
        ProductControl productControl;

        public ProductWindow(Order order, bool isNew, bool canEdit)
        {
            InitializeComponent();
            if (order != null)
            {
                this.order = order;
                if (isNew)
                    productControl = new ProductControl(true, new Product(), null, null);
                else if (order.Product != null)
                    productControl = new ProductControl(true, order.Product, null, null);
            }
            else { productControl = App.productControl; }

            MyPanel.IsEnabled = canEdit;

            MyPanel.Children.Add(productControl);
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
            {
                Methods.TakeWarning("Вы не заполнили все нужные поля!");
                return;
            }

            if (Save())
            {
                
                this.Close();
                Methods.TakeInformation("Успешно сохранено!");
            }
        }

        private bool Validate()
        {
            return productControl.CheckData();
        }

        private bool Save()
        {
            try
            {
                //Save products (каскадный метод)
                productControl.SaveProduct();

                //Save hierarchy (product detail) (каскадный метод)
                productControl.SaveHierarchy();

                App.db.SaveChanges();
                App.productControl = productControl;

                return true;
            }
            catch (Exception ex) { Methods.TakeWarning($"Ошибка при сохранении!\n{ex.Message}"); return false; }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!Methods.TakeChoice("Вы точно хотите закрыть окно изделий?"))
                e.Cancel = true;
            else
            {
                MyPanel.Children.Clear();
                e.Cancel = false;
            }
        }
    }
}

using System;
using System.Linq;
using System.Windows;
using UchebnayaPractica.Database;
using UchebnayaPractica.Pages;

namespace UchebnayaPractica.Windows
{
    /// <summary>
    /// Логика взаимодействия для StatusWindow.xaml
    /// </summary>
    public partial class StatusWindow : Window
    {
        Order order;
        StatusOrder statusOrder;
        public StatusWindow(Order order)
        {
            InitializeComponent();
            this.order = order;
            statusOrder = new StatusOrder()
            {
                IdOldStatus = order.CurrentStatus.IdStatus,
                OrderNumber = order.OrderNumber,
            };

            OldStatusCb.ItemsSource = App.db.OrderStatus.ToList();

            if (App.currentUser.RoleId != 4 && App.currentUser.RoleId != 2 && App.currentUser.RoleId != 1)
                NewStatusCb.ItemsSource = App.db.OrderStatus.Where(x => x.Id == 2 || (order.CurrentStatus.IdStatus != 1 && x.Id == order.CurrentStatus.IdStatus + 1)
                || (order.CurrentStatus.IdStatus == 1 && x.Id == 3)).ToList();
            else if(App.currentUser.RoleId == 2)
                NewStatusCb.ItemsSource = App.db.OrderStatus.Where(x => x.Id == 4).ToList();
            else if (App.currentUser.RoleId == 1)
                NewStatusCb.ItemsSource = App.db.OrderStatus.Where(x => x.Id == order.CurrentStatus.IdStatus + 1).ToList();
            else
                NewStatusCb.ItemsSource = App.db.OrderStatus.Where(x => x.Id == 2).ToList();


            DataContext = statusOrder;
            OldStatusCb.SelectedItem = order.CurrentStatus.OrderStatus;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewStatusCb.SelectedIndex == -1)
                {
                    Methods.TakeWarning("Вы не выбрали новый статус!");
                    return;
                }
                if (DescriptionPanel.Visibility == Visibility.Visible && DescriptionTb.Text == "")
                {
                    Methods.TakeWarning("Вы не указали причину отмены заказа!");
                    return;
                }

                statusOrder.DateChange = DateTime.Now.Date;
                statusOrder.TimeChange = DateTime.Now.TimeOfDay;

                if (order.LoginManager == null)
                    order.LoginManager = App.currentUser.Login;

                App.db.StatusOrder.Add(statusOrder);
                App.db.SaveChanges();
                App.mainWindow.MainFrame.Navigate(new OrderPage());
                this.Close();
                Methods.TakeInformation("Статус успешно изменен!");
            }
            catch(Exception ex) { Methods.TakeWarning($"Не удалось сохранить новый статус!\n{ex.Message}"); }
        }

        private void NewStatusCb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            OrderStatus status = NewStatusCb.SelectedItem as OrderStatus;
            if(status.Id == 2)
                DescriptionPanel.Visibility = Visibility.Visible;
            else
                DescriptionPanel.Visibility = Visibility.Collapsed;
            statusOrder.IdStatus = status.Id;
        }
    }
}

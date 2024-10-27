using System.Collections.Generic;
using System.Linq;
using System.Windows;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Windows
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            IEnumerable<StatusOrder> statuses = App.db.StatusOrder;
            if (App.currentUser.RoleId == 5)
                statuses = statuses.Where(x => x.Order.LoginManager == null || x.Order.LoginManager == App.currentUser.Login);

            MyList.ItemsSource = statuses.ToList();
        }
    }
}

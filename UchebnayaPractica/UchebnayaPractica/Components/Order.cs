using System;
using System.Linq;
using System.Windows;

namespace UchebnayaPractica.Database
{
    public partial class Order
    {
        public StatusOrder CurrentStatus
        {
            get { return StatusOrder.First(y => y.Id == StatusOrder.Max(x => x.Id)); }
        }

        public DateTime GetOrderDate
        {
            get
            {
                if (DateOrder == new DateTime())
                    return DateTime.Now.Date;
                return DateOrder.Date;
            }
        }

        public Visibility CanDelete
        {
            get
            {
                if(CurrentStatus.IdStatus == 1 && (App.currentUser.RoleId == 4 || App.currentUser.RoleId == 5))
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }
        }
    }
}

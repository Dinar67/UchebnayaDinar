using System.Windows;
using UchebnayaPractica.Database;
using UchebnayaPractica.Pages;

namespace UchebnayaPractica
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UchebkaEntities db = new UchebkaEntities();
        public static MainWindow mainWindow;
        public static User currentUser;
        public static EmployeePage employeePage;
    }
}

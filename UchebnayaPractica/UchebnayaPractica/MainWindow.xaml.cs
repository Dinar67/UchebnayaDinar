using System.IO;
using System.Linq;
using System.Windows;
using UchebnayaPractica.Database;

namespace UchebnayaPractica
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.mainWindow = this;


            LoadImage();

        }

        private void LoadImage()
        {
            string path = @"C:\Users\user\Desktop\Учебная практика\Сессия1\Ресурсы - Сессия 1\data\Изображения\Фото пользователей";

            string[] files = Directory.GetFiles(path);
            foreach(var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                User user = App.db.User.FirstOrDefault(x => x.Login == fileName);
                if(user != null)
                {
                    UserImage image = App.db.UserImage.Add(new UserImage()
                    {
                        Photo = File.ReadAllBytes(file),
                    });
                    App.db.SaveChanges();
                    user.IdUserImage = image.Id;
                    App.db.SaveChanges();
                }
            }
            App.db.SaveChanges();
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

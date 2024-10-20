using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        User user = new User() { RoleId = 4 };
        UserImage image = new UserImage();
        public RegPage()
        {
            InitializeComponent();
        }

        private void LoadImageBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var opn = new OpenFileDialog();
            opn.Title = "Выберите изображение";
            opn.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files|*.*";
            if(opn.ShowDialog() == true)
            {
                image.Photo = File.ReadAllBytes(opn.FileName);
                if(image.Id == 0)
                    App.db.UserImage.Add(image);
                user.IdUserImage = image.Id;
                MainImage.Source = Methods.GetBitmapImageFromBytes(image.Photo);
            }
        }

        private void DeleteImageBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (image.Id == 0)
                image.Photo = null;
            else
            {
                App.db.UserImage.Remove(image);
                image = new UserImage();
            }
            user.IdUserImage = null;
            MainImage.Source = null;
        }

        private void RegBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Regex FIO = new Regex(@"^[А-ЯA-Z][а-яa-z]*$");
            if (LastNameTb.Text == "" || !FIO.IsMatch(LastNameTb.Text))
            {
                Methods.TakeWarning("Вы неправильно ввели фаимлию!");
                return;
            }

            if (txtFirstName.Text == "" || !FIO.IsMatch(txtFirstName.Text))
            {
                Methods.TakeWarning("Вы неправильно ввели имя!");
                return;
            }
            if (txtMiddleName.Text == "" || !FIO.IsMatch(txtMiddleName.Text))
            {
                Methods.TakeWarning("Вы неправильно ввели отчество!");
                return;
            }

            if (App.db.User.Any(x => x.Login == txtLogin.Text))
            {
                Methods.TakeWarning("Этот логин уже используется!");
                return;
            }
            if (txtPassword.Password == "")
            {
                Methods.TakeWarning("Вы не ввели пароль!");
                return;
            }
            if(txtPassword.Password.Length < 5)
            {
                Methods.TakeWarning("Пароль должен содержать не меньше 5 символов!");
                return;
            }
            string message = ValidatePassword(txtPassword.Password);
            if(message != "")
            {
                Methods.TakeWarning(message);
                return;
            }

            user.LastName = LastNameTb.Text;
            user.FirstName = txtFirstName.Text;
            user.Patronymic = txtMiddleName.Text;
            user.Login = txtLogin.Text;
            user.Password = txtPassword.Password;
            App.db.User.Add(user);

            App.db.SaveChanges();

            NavigationService.Navigate(new AuthPage());
            Methods.TakeInformation("Вы зарегистрированны!");
        }
        private string ValidatePassword(string password)
        {
            if (password.Length < 4 || password.Length > 16)
                return "Пароль должен содержать от 4 до 16 символов.";
            const string forbiddenChars = "*&{}|+";
            if (forbiddenChars.Any(x => password.Contains(x)))
                return "Пароль не должен содержать символы *, &, {, }, |, +.";
            if (!Regex.IsMatch(password, "[A-Z]"))
                return "Пароль должен содержать хотя бы одну заглавную букву.";
            if (!Regex.IsMatch(password, @"\d"))
                return "Пароль должен содержать хотя бы одну цифру.";

            return "";
        }

        private void Back_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Page
    {
        User user;
        bool isNew;
        string oldLogin;
        List<Operation> operations;
        public AddEditEmployee(User user,bool isNew, string title = "Добавить сотрудника")
        {
            InitializeComponent();
            TitleTb.Text = title;
            oldLogin = user.Login;
            RoleCb.ItemsSource = App.db.Role.Where(x => x.Id != 4).ToList();
            RoleCb.SelectedItem = user.Role;
            this.user = user;
            this.isNew = isNew;
            DataContext = this.user;

            operations = user.Operation.ToList();
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Operation> operations = this.operations;
            MyList.ItemsSource = operations.ToList();
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            operations.Add(new Operation());
            Refresh();
        }

        private void Back_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void OperationTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            (textBox.DataContext as Operation).Name = textBox.Text;
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string mistake = "";

            if (LoginTb.Text == "" && mistake == "")
                mistake = "Вы не заполнили логин!";
            if (PasswordTb.Text == "" && mistake == "")
                mistake = "Вы не заполнили пароль!";
            mistake = ValidatePassword(PasswordTb.Text);
            if (PasswordTb.Text == "" && mistake == "")
                mistake = "Вы не заполнили логин!";
            if (App.db.User.Any(x => x.Login == LoginTb.Text) && (oldLogin != LoginTb.Text || isNew) && mistake == "")
                mistake = "Такой логин уже есть!";
            if (RoleCb.SelectedIndex == -1 && mistake == "")
                mistake = "Вы не выбрали роль!";
            if (LastNameCb.Text == "" && mistake == "")
                mistake = "Вы не заполнили фамилию!";
            if (FirstNameCb.Text == "" && mistake == "")
                mistake = "Вы не заполнили имя!";
            if (PatronymicCb.Text == "" && mistake == "")
                mistake = "Вы не заполнили отчество!";

            if (mistake != "")
            {
                Methods.TakeWarning(mistake);
                return;
            }

            user.RoleId = (RoleCb.SelectedItem as Role).Id;
            if (isNew)
                user = App.db.User.Add(user);

            foreach(var operation in operations)
            {
                if (operation.Id == 0)
                {
                    operation.Login = user.Login;
                    App.db.Operation.Add(operation);
                }
            }

            App.db.SaveChanges();
            NavigationService.GoBack();
            Methods.TakeInformation("Изменения успешно сохранены!");
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

        private void Delete_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            Operation operation = image.DataContext as Operation;

            operations.Remove(operation);
            if(operation.Id != 0)
                App.db.Operation.Remove(operation);
            Refresh();
        }
    }
}

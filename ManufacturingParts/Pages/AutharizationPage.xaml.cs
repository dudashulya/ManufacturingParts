using ManufacturingParts.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManufacturingParts.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutharizationPage.xaml
    /// </summary>
    public partial class AutharizationPage : Page
    {
        public AutharizationPage()
        {
            InitializeComponent();
            CheckSavedCredentials();
        }

        private void CheckSavedCredentials()
        {
            if (File.Exists("credentials.txt"))
            {
                string[] credentials = File.ReadAllLines("credentials.txt");
                txtLogin.Text = credentials[0];
                txtPassword.Password = credentials[1];
                chkRememberMe.IsChecked = true;
                btnLogin_Click(null, null);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;
           

            if (AuthenticateUser(login, password, out string role, out string fullName))
            {
                if (chkRememberMe.IsChecked == true)
                {
                    File.WriteAllLines("credentials.txt", new string[] { login, password });
                }
                else
                {
                    File.Delete("credentials.txt");
                }

                switch (role)
                {
                    case "Заказчик":
                        ShowUserScreen("Экран заказчика", fullName);
                       
                        
                        break;
                    case "Менеджер":
                        ShowUserScreen("Экран менеджера", fullName); 
                        NavigationService.Navigate(new ManagerPage());
                        break;
                    case "Конструктор":
                        ShowUserScreen("Экран конструктора", fullName);
                        NavigationService.Navigate(new DesignerPage());
                        break;
                    case "Мастер":
                        ShowUserScreen("Экран мастера", fullName);
                        NavigationService.Navigate(new MasterPage());
                        break;
                    case "Директор":
                        ShowUserScreen("Экран директора", fullName);
                        NavigationService.Navigate(new DirectorPage());
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool AuthenticateUser(string login, string password, out string role, out string fullName)
        {
            role = string.Empty;
            fullName = string.Empty;
            using (var db = new UchebkaKornilovaEntities())
            {
                var user = db.User.FirstOrDefault(u => u.Login == login && u.Password == password);
                if (user != null)
                {
                    role = user.Role.Name;
                    fullName = $"{user.FirstName} {user.LastName}";
                    return true;
                }
            }
            return false;
        }

        private void ShowUserScreen(string screenTitle, string fullName)
        {
            MessageBox.Show($"Добро пожаловать, {fullName}! Вы перенаправлены на {screenTitle}.", "Успешная авторизация");
            
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}

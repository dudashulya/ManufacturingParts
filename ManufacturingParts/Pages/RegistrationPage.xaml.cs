using ManufacturingParts.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = txtRegLogin.Text;
            string password = txtRegPassword.Password;
            string confirmPassword = txtRegConfirmPassword.Password;

            if (!ValidatePassword(password))
            {
                MessageBox.Show("Пароль не соответствует требованиям.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RegisterUser(login, password))
            {
                MessageBox.Show("Регистрация прошла успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new AutharizationPage());
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private bool ValidatePassword(string password)
        {
            if (password.Length < 4 || password.Length > 16)
                return false;

            if (Regex.IsMatch(password, @"[*&{}|+]"))
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"\d"))
                return false;

            return true;
        }

        private bool RegisterUser(string login, string password)
        {
            using (var db = new UchKornilovaEntities())
            {
                var existingUser = db.User.FirstOrDefault(u => u.Login == login);
                if (existingUser != null)
                    return false;

                var newUser = new User
                {
                    Login = login,
                    Password = password,
                    RoleId = 4
                };

                db.User.Add(newUser);
                db.SaveChanges();
            }
            return true;
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

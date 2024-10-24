using ManufacturingParts.Components;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ManufacturingParts.DataBase;

using static System.Net.Mime.MediaTypeNames;

namespace ManufacturingParts.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        UserImage image = new UserImage();
        User user = new User();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = txtRegLogin.Text;
            string password = txtRegPassword.Password;
            string confirmPassword = txtRegConfirmPassword.Password;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string patronymic = txtPatronymic.Text;

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

            if (RegisterUser(login, password, firstName, lastName,patronymic))
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

        private bool RegisterUser(string login, string password, string firstName, string lastName, string patronymic)
        {
            using (var db = new UchebkaKornilovaEntities1())
            {
                var existingUser = db.User.FirstOrDefault(u => u.Login == login);
                if (existingUser != null)
                    return false;

                var newUser = new User
                {
                    Login = login,
                    Password = password,
                    RoleId = 4,
                    LastName = lastName,
                    FirstName = firstName,
                    Patronymic = patronymic
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

        private void LoadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var opn = new OpenFileDialog();
            opn.Title = "Выберите изображение";
            opn.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff|All Files|*.*";
            if (opn.ShowDialog() == true)
            {
                image.Photo = File.ReadAllBytes(opn.FileName);
                if (image.Id == 0)
                    App.db.UserImage.Add(image);
                user.IdUserImage = image.Id;
                MainImage.Source = Methods.GetBitmapImageFromBytes(image.Photo);
            }
        }

        private void DeleteImageBtn_Click(object sender, RoutedEventArgs e)
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
    }
}

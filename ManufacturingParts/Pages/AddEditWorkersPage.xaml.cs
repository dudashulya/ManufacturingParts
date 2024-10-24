using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using ManufacturingParts.DataBase;
using ManufacturingParts.Components;
using Microsoft.Win32;
using System.IO;

namespace ManufacturingParts.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditWorkersPage.xaml
    /// </summary>
    public partial class AddEditWorkersPage : Page
    {
        User user;
        UserImage userImage;
        bool isNew;
        string oldLogin;
        List<Operation> operations;
        public AddEditWorkersPage(User user, bool isNew, string title = "Добавить сотрудника")
        {
            if (!App.mainWindow.MyFrame.CanGoBack)
                Back.Visibility = Visibility.Collapsed;

            //if (App.currentUser.RoleId == 1 || App.currentUser.RoleId == 2 || App.currentUser.RoleId == 5)
            //{
            //    MainPanel.IsEnabled = false;
            //    SaveBtn.Visibility = Visibility.Collapsed;
            //    AddBtn.Visibility = Visibility.Collapsed;
            //    RoleCb.ItemsSource = App.db.Role.Where(x => x.Id != 4).ToList();
            //}
            //else if (App.currentUser.RoleId == 4)
            //{
            //    PhotoPanel.Visibility = Visibility.Visible;
            //    PostPanel.Visibility = Visibility.Collapsed;
            //    EducationPanel.Visibility = Visibility.Collapsed;
            //    OperationPanel.Visibility = Visibility.Collapsed;
            //    RoleCb.ItemsSource = App.db.Role.ToList();
            //    RoleCb.IsEnabled = false;
            //    if (user.IdUserImage != null)
            //        MainImage.Source = Methods.GetBitmapImageFromBytes(user.UserImage.Photo);
            //}
            else
                RoleCb.ItemsSource = App.db.Role.Where(x => x.Id != 4).ToList();

            TitleTb.Text = title;
            oldLogin = user.Login;
            RoleCb.SelectedItem = user.Role;
            this.user = user;
            this.isNew = isNew;
            DataContext = this.user;

            //operations = user.Operation.ToList();
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Operation> operations = this.operations;
            MyList.ItemsSource = operations.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            operations.Add(new Operation());
            Refresh();
        }

        private void OperationTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            (textBox.DataContext as Operation).Name = textBox.Text;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
                App.workersPage = null;
            }
        }

        private void DeleteImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}

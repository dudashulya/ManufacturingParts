using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {
        public WorkersPage()
        {
            InitializeComponent();
            LoadEmployees();
        }
        private void LoadEmployees()
        {

            var employees = App.db.User.Where(u => u.RoleId != 4).ToList();

            EmployeesGrid.ItemsSource = employees;
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditWorkersPage());
        }
    }
}

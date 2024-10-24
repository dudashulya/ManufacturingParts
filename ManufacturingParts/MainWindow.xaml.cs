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
using ManufacturingParts.Pages;

namespace ManufacturingParts
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            App.mainWindow = this;
           
            if (Instance == null)
                Instance = this;
            else
                return;

            if (App.currentUser != null)
            {
                MyFrame.NavigationService.Navigate(App.GetRightPage());
                return;
            }

             MyFrame.NavigationService.Navigate(new AutharizationPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

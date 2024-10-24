﻿using System;
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
    /// Логика взаимодействия для DirectorPage.xaml
    /// </summary>
    public partial class DirectorPage : Page
    {
        public DirectorPage()
        {
            InitializeComponent();
        }

        private void MaterialBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MaterialPage());
        }

        private void CustomerBtn_Click(object sender, RoutedEventArgs e)
        {
             NavigationService.Navigate(new WorkersPage());
        }
    }
}

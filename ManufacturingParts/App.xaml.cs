using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ManufacturingParts.Components;
using ManufacturingParts.Pages;
using ManufacturingParts.DataBase;
using System.Windows.Controls;

namespace ManufacturingParts
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UchebkaKornilovaEntities1 db = new UchebkaKornilovaEntities1();
        public static WorkersPage workersPage;
        public static User currentUser;
        public static MainWindow mainWindow;

        public static Page GetRightPage()
        {
            switch (currentUser.RoleId)
            {
                case 1:
                    return new DirectorPage();
                case 2:
                    return new DesignerPage();
                case 3:
                    return new ManagerPage();
                case 4:
                    return new MasterPage();
                case 5:
                    return new WorkersPage();
                default:
                    return new AutharizationPage();
            }
        }
    }
}

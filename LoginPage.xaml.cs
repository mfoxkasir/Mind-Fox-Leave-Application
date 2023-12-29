using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using MindFoxEmployeeData;

namespace Mind_Fox_Leave_Application
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public ICommand LoginCommand { get; private set; }

        public LoginPage(EmployeeDataList employeeDataList)
        {
            InitializeComponent();
            //LoginCommand = new RelayCommand(Login, CanLogin);
        }

        //private bool CanLogin(object sender, ExecutedRoutedEventArgs e)
        //{

        //}
        //
        private void Login()
        {
            
        }
    }
}

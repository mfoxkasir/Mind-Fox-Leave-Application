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

using Mind_Fox_data;

namespace Mind_Fox_Leave_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private EmployeeList MindFox = new EmployeeList();

        private void EmployeeId_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string enteredText = textBox.Text;
            int textLength = enteredText.Length;

            if (textLength < 4 || textLength > 4)
            {
                MessageBox.Show("Employee ID cannot be less than or greater than 4 digits");
            }
            else if (textLength == 4 && !MindFox.ContainsEmployeeWithID(enteredText))
            {
                MessageBox.Show(enteredText + " is not in the Employee ID list.");
            }
        }
    }
}

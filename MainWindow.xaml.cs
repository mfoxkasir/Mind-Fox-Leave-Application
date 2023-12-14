using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
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

        // Fetch EmployeeList from CSV
        static private EmployeeList MindFox = new EmployeeList();

        // Fetch Employee Names
        private List<string> EmployeeNames = MindFox.GetEmployeeNames();

        // Variable for storing selected employee
        private Employee SelectedEmployee;

        // Employee Name List Generation 
        private void EmployeeName_GotFocus(object sender, RoutedEventArgs e)
        {
            EmployeeName.ItemsSource = EmployeeNames;
        }

        private void EmployeeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string EmpName = EmployeeName.SelectedItem as string;
            foreach (Employee Emp in MindFox.EmpList)
            {
                if (Emp.Name == EmpName)
                {
                    SelectedEmployee = Emp;
                    EmployeeId.Text = Emp.ID;
                    EarnedLeave.SetBinding(TextBlock.TextProperty, new Binding(Emp.EarnedLeave.ToString()));
                    CasualLeave.Text = Emp.CasualLeave.ToString();
                    PersonalLeave.Text = Emp.PersonalLeave.ToString();
                    break;
                }
            }
        }

        private void EmployeeName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!EmployeeName.Items.Contains(EmployeeName.Text))
            {
                EmployeeName.Text = null;
                EmployeeId.Text = null;
            }
        }

        private void ReportingTo_GotFocus(object sender, RoutedEventArgs e)
        {
            List<string> EmployeeNames = MindFox.GetAdminNames();
            ReportingTo.ItemsSource = EmployeeNames;
        }

        private void FromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime thirtyDaysAgo = DateTime.Today.AddDays(-30);
            int Days = DaysCalculator(); 

            if (FromDate.SelectedDate < thirtyDaysAgo)
            {
                MessageBox.Show("Please select a date within the last 30 days.");
                FromDate.SelectedDate = DateTime.Today;
            }
            else if (ToDate.SelectedDate < FromDate.SelectedDate)
            {
                MessageBox.Show("'To' date cannot be before than 'From' date.");
                FromDate.SelectedDate = ToDate.SelectedDate;
            }
            else if (Days != 0 && LeaveType.SelectedItem != null && SelectedEmployee != null)
            {
                if (!LeaveValidityChecker(LeaveType.SelectedIndex, Days, SelectedEmployee))
                {
                    MessageBox.Show("This type of leave cannot be taken. Try Selecting different type.");
                }
            }
        }

        private void ToDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            int Days = DaysCalculator();
            if (FromDate.SelectedDate == null) 
            {
                MessageBox.Show("Select 'From' date first.");
                ToDate.SelectedDate = null;
            }
            else if (ToDate.SelectedDate < FromDate.SelectedDate)
            {
                MessageBox.Show("'To' date cannot be before than 'From' date.");
                ToDate.SelectedDate = FromDate.SelectedDate;
            }
            else if (Days != 0 && LeaveType.SelectedItem != null && SelectedEmployee != null)
            {
                if (!LeaveValidityChecker(LeaveType.SelectedIndex, Days, SelectedEmployee))
                {
                    MessageBox.Show("This type of leave cannot be taken. Try Selecting different type.");
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int Days = DaysCalculator();
            if (EmployeeName.SelectedItem != null && ReportingTo.SelectedItem != null && FromDate.SelectedDate != null && ToDate.SelectedDate != null && LeaveType.SelectedItem != null)
            {
                LeaveCalculator(LeaveType.SelectedIndex, Days, SelectedEmployee);
                MindFox.EmployeeWriter();
                MessageBox.Show("Your Leave is approved.");
            }
        }

        private void LeaveType_DropDownOpened(object sender, EventArgs e)
        {
            List<string> leaveTypes = new List<string>() { "Earned Leave", "Casual Leave", "Personal Leave" };
            LeaveType.ItemsSource = leaveTypes;
        }

        private void LeaveType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Days = DaysCalculator();
            if (Days != 0 && SelectedEmployee != null)
            {
                if (!LeaveValidityChecker (LeaveType.SelectedIndex, Days, SelectedEmployee))
                {
                    MessageBox.Show("This type of leave cannot be taken. Try Selecting different type.");
                    LeaveType.SelectedItem = null;
                }
            }
            else if (SelectedEmployee == null)
            {
                MessageBox.Show("Select the Employee Name.");
                LeaveType.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Please select both From and To dates.");
                LeaveType.SelectedItem = null;
            }
        }

        private bool LeaveValidityChecker(int leaveType, int days, Employee emp)
        {
            switch (leaveType)
            {
                case 0:
                    return emp.EarnedLeave >= days;
                case 1:
                    return emp.CasualLeave >= days;
                case 2:
                    return emp.PersonalLeave >= days;
                default:
                    return false;
            }
        }

        private void LeaveCalculator(int leaveType, int days, Employee emp)
        {
            switch (leaveType)
            {
                case 0:
                    emp.EarnedLeave -= days;
                    break;
                case 1:
                    emp.CasualLeave -= days;
                    break;
                case 2:
                    emp.PersonalLeave -= days;
                    break;
            }
        }

        private int DaysCalculator()
        {
            if (ToDate.SelectedDate.HasValue && FromDate.SelectedDate.HasValue)
            {
                int Days = (ToDate.SelectedDate.Value - FromDate.SelectedDate.Value).Days + 1;
                return Days;
            } 
            else
            {
                return 0;
            }
        }
    }
}

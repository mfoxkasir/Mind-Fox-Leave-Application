﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
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
using System.Xml.Serialization;
using ReadWriteXML;
using MindFoxEmployeeData;
using Mind_Fox_Leave_Application.ApplicationSpecificClasses;

namespace Mind_Fox_Leave_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private variables

        // Fetch EmployeeList from CSV
        private EmployeeDataList mindFoxEmployeeDataList;

        private EmployeeQuotaList mindFoxEmployeeQuotaList;

        private EmployeeLeaveLogger mindFoxLeaveLogger;

        // Store selected employee
        //private Employee currentEmployee;

        private ReadWriteToXML readWriteToXML;
        private string commonXML = "CommonXML";
        private string employeeXML = "EmployeeXML";
        private string employeeDataFileName = "EmployeeData.xml";
        private string employeeQuotaFileName = "EmployeeQuota.xml";
        private string employeeDataFilePath;
        private string employeeQuotaFilePath;
        // Log the submit event into log file
        //private LeaveLogger log;

        #endregion

        #region Properties


        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.readWriteToXML = new ReadWriteToXML();

            employeeDataFilePath = readWriteToXML.XMLFilePathMaker(commonXML, employeeDataFileName);
            this.mindFoxEmployeeDataList = EmployeeDataList.getInstance(employeeDataFilePath);

            employeeQuotaFilePath = readWriteToXML.XMLFilePathMaker(commonXML, employeeQuotaFileName);
            this.mindFoxEmployeeQuotaList = EmployeeQuotaList.getInstance(employeeQuotaFilePath);

            readWriteToXML.AppendObjectToXml(mindFoxEmployeeDataList.ListOfEmployees, employeeDataFilePath);

            this.mindFoxLeaveLogger = new EmployeeLeaveLogger();


            // MainFrame.NavigationService.Navigate(new LeaveHistory(mindFoxEmployeeDataList));
            mainFrame.NavigationService.Navigate(new LoginPage(mindFoxEmployeeDataList));
            //this.mindFox = new EmployeeList("data.csv");
            //this.DataContext = this.mindFox;

            //string EmployeeDataFilePath =  readWriteToXML.XMLFilePathMaker("CommonXML", "EmployeeData.xml");
            //this.mindFox = EmployeeDataList.getInstance(EmployeeDataFilePath);
            // this.mindFoxQuotaList = EmployeeQuotaList.getInstance(EmployeeDataFilePath);

            return;
        }

        #region Private methods
            //private void EmployeeDetail_SelectionChanged(object sender, EventArgs e)
            //{
            //    if (EmployeeName.SelectedItem != null)
            //    {
            //        string EmpName = EmployeeName.SelectedItem as string;
            //        //this.mindFox.CurrentEmployee = null;
            //        this.mindFox.CurrentEmployee = this.mindFox.ListofEmployees.FirstOrDefault(emp => emp.Name == EmpName);
            //        currentEmployee = this.mindFox.CurrentEmployee;
            //    }
            //}

            //private void EmployeeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //{
            //    if (EmployeeName.SelectedItem != null)
            //    {
            //        string EmpName = EmployeeName.SelectedItem as string;
            //        this.mindFox.CurrentEmployee = null;
            //        this.mindFox.CurrentEmployee = this.mindFox.ListofEmployees.FirstOrDefault(emp => emp.Name == EmpName);
            //        currentEmployee = this.mindFox.CurrentEmployee;
            //    }
            //}

            //private void EmployeeName_LostFocus(object sender, RoutedEventArgs e)
            //{
            //    if (!EmployeeName.Items.Contains(EmployeeName.Text))
            //    {
            //        EmployeeName.Text = null;
            //        EmployeeId.Text = null;
            //    }
            //    for (int i = 0; i < EmployeeName.Items.Count; i++)
            //    {
            //        if ((EmployeeName.Items[i] == EmployeeName.Text))
            //            break;
            //    }

            //}

            //private void FromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            //{
            //    if (FromDate.SelectedDate != null)
            //    {
            //        int Days = DaysCalculator();

            //        DateTime thirtyDaysAgo = DateTime.Today.AddDays(-30);
            //        if (FromDate.SelectedDate < thirtyDaysAgo)
            //        {
            //            MessageBox.Show("Please select a date within the last 30 days.");
            //            FromDate.SelectedDate = DateTime.Today;
            //            return;
            //        }

            //        if (Days < 1 && ToDate.SelectedDate != null)
            //        {
            //            MessageBox.Show("'To' date cannot be before than 'From' date.");
            //            ToDate.SelectedDate = FromDate.SelectedDate;
            //        }
            //        else if (LeaveType.SelectedItem != null)
            //        {
            //            if (!currentEmployee.LeaveValidityChecker(LeaveType.SelectedIndex, Days))
            //            {
            //                MessageBox.Show("This type of leave cannot be taken. Try Selecting different type.");
            //                FromDate.SelectedDate = ToDate.SelectedDate;
            //            }
            //        }
            //    }
            //}

            //private void ToDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            //{
            //    if (ToDate.SelectedDate != null)
            //    {
            //        int Days = DaysCalculator();

            //        if (FromDate.SelectedDate == null)
            //        {
            //            MessageBox.Show("Select 'From' date first.");
            //            this.ToDate.SelectedDateChanged -= this.ToDate_SelectedDateChanged;
            //            ToDate.SelectedDate = null;
            //            this.ToDate.SelectedDateChanged += this.ToDate_SelectedDateChanged;
            //        }
            //        else if (Days < 1 && FromDate.SelectedDate != null)
            //        {
            //            MessageBox.Show("'To' date cannot be before than 'From' date.");
            //            ToDate.SelectedDate = FromDate.SelectedDate;
            //        }
            //        else if (LeaveType.SelectedItem != null)
            //        {
            //            if (!currentEmployee.LeaveValidityChecker(LeaveType.SelectedIndex, Days))
            //            {
            //                MessageBox.Show("This type of leave cannot be taken. Try Selecting different type.");
            //                ToDate.SelectedDate = FromDate.SelectedDate;
            //            }
            //        }
            //    }
            //}

            //private void LeaveType_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //{
            //    if (LeaveType.SelectedItem != null)
            //    {
            //        int Days = DaysCalculator();

            //        if (currentEmployee == null)
            //        {
            //            MessageBox.Show("Select the Employee Name.");
            //            this.LeaveType.SelectionChanged -= this.LeaveType_SelectionChanged;
            //            LeaveType.SelectedItem = null;
            //            this.LeaveType.SelectionChanged += this.LeaveType_SelectionChanged;
            //            return;
            //        }

            //        if (Days > 0)
            //        {
            //            if (!currentEmployee.LeaveValidityChecker(LeaveType.SelectedIndex, Days))
            //            {
            //                MessageBox.Show("This type of leave cannot be taken. Try Selecting different type.");
            //                this.LeaveType.SelectionChanged -= this.LeaveType_SelectionChanged;
            //                LeaveType.SelectedItem = null;
            //                this.LeaveType.SelectionChanged += this.LeaveType_SelectionChanged;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please select both From and To dates.");
            //            this.LeaveType.SelectionChanged -= this.LeaveType_SelectionChanged;
            //            LeaveType.SelectedItem = null;
            //            this.LeaveType.SelectionChanged += this.LeaveType_SelectionChanged;
            //        }
            //    }
            //}

            //private void Submit_Click(object sender, RoutedEventArgs e)
            //{
            //    int Days = DaysCalculator();

            //    if (EmployeeName.SelectedItem != null && ReportingTo.SelectedItem != null && FromDate.SelectedDate != null && ToDate.SelectedDate != null && LeaveType.SelectedItem != null)
            //    {
            //        string report = $"Employee: {EmployeeName.SelectedItem} (ID: {EmployeeId.Text})\r\nLeave Type: {LeaveType.SelectedItem}\r\nNumber of Days: {Days}\r\nFrom: {FromDate.SelectedDate?.ToShortDateString()} To: {ToDate.SelectedDate?.ToShortDateString()}\r\nReporting to: {ReportingTo.SelectedItem}\r\nReason: {Reason.Text}";
            //        string confirmMessage = $"Confirmation:\r\n{report}\r\n\r\nAre you sure you want to request this leave?";
            //        string title = "Confirmation Window";
            //        MessageBoxButton buttons = MessageBoxButton.YesNo;
            //        MessageBoxResult result = MessageBox.Show(confirmMessage, title, buttons);

            //        if (result == MessageBoxResult.Yes)
            //        {
            //            currentEmployee.LeaveDeductor(LeaveType.SelectedIndex, Days);
            //            mindFox.csvLeaveUpdater();
            //            log.LogLeave(report);
            //            MessageBox.Show("Your Leave is approved.");
            //            //this.DataContext = null;
            //            //ResetUIComponents(this);
            //        }
            //    }
            //}

            //private int DaysCalculator()
            //{
            //    if (ToDate.SelectedDate.HasValue && FromDate.SelectedDate.HasValue)
            //    {
            //        int Days = (ToDate.SelectedDate.Value - FromDate.SelectedDate.Value).Days + 1;
            //        return Days;
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}

            //private void ResetUIComponents(DependencyObject depObj)
            //{
            //    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            //    {
            //        var child = VisualTreeHelper.GetChild(depObj, i);

            //        if (child is TextBox textBox)
            //        {
            //            textBox.Text = string.Empty; // Reset textboxes
            //        }
            //        else if (child is ComboBox checkBox)
            //        {
            //            checkBox.SelectedItem = null; // Reset checkboxes
            //        }
            //        else if (child is DatePicker datePicker)
            //        {
            //            datePicker.SelectedDate = null; // Reset DatePickers
            //        }
            //        // Add conditions for other types of UI components (labels, dropdowns, etc.)

            //        ResetUIComponents(child);
            //    }
            //}

            #endregion
    }

}
using System;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Windows.Markup;
using System.Xml.Linq;

namespace Mind_Fox_data
{
    public class Employee : INotifyPropertyChanged
    {

        #region Eventhandlers

        // Implement PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public Properties

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string id;
        public string ID
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private bool canReport;
        public bool CanReport
        {
            get { return canReport; }
            set
            {
                if (value != canReport)
                {
                    canReport = value;
                    OnPropertyChanged(nameof(CanReport));
                }
            }
        }

        private int earnedLeave;
        public int EarnedLeave
        {
            get { return earnedLeave; }
            set
            {
                if (value != earnedLeave)
                {
                    earnedLeave = value;
                    OnPropertyChanged(nameof(EarnedLeave));
                }
            }
        }

        private int casualLeave;
        public int CasualLeave
        {
            get { return casualLeave; }
            set
            {
                if (value != casualLeave)
                {
                    casualLeave = value;
                    OnPropertyChanged(nameof(CasualLeave));
                }
            }
        }

        private int personalLeave;
        public int PersonalLeave
        {
            get { return personalLeave; }
            set
            {
                if (value != personalLeave)
                {
                    personalLeave = value;
                    OnPropertyChanged(nameof(PersonalLeave)) ;
                }
            }
        }

        #endregion

        #region Public methods

        public void LeaveDeductor(int leaveType, int days)
        {
            switch (leaveType)
            {
                case 0:
                    EarnedLeave -= days;
                    break;
                case 1:
                    CasualLeave -= days;
                    break;
                case 2:
                    PersonalLeave -= days;
                    break;
            }
        }

        public bool LeaveValidityChecker(int leaveType, int days)
        {
            switch (leaveType)
            {
                case 0:
                    return EarnedLeave >= days;
                case 1:
                    return CasualLeave >= days;
                case 2:
                    return PersonalLeave >= days;
                default:
                    return false;
            }
        }
    }

    #endregion

    public class EmployeeList : INotifyPropertyChanged
    {

        #region EventHandlers

        // Implement PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Public variables

        public List<Employee> ListofEmployees = new List<Employee>();

        #endregion

        #region Private variables

        private string csvFilePath;

        #endregion

        #region Public properties

        public List<string> EmployeeNames
        {
            get
            {
               return this.GetEmployeeNames();
            }
        }

        public List<string> AdminNames
        {
            get
            {
                return this.GetAdminNames();
            }
        }

        public List<string> LeaveTypes
        {
            get
            {
                return this.GetLeaveTypes();
            }
        }


        private Employee currentEmployee;
        public Employee CurrentEmployee {
            get { return currentEmployee; }
            set
            {
                if (value != currentEmployee)
                {
                    currentEmployee = value;
                    OnPropertyChanged(nameof(CurrentEmployee));
                }
            }
        }

        #endregion


        public EmployeeList(string fileName)
        {
            csvFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, fileName);

            if (File.Exists(csvFilePath))
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    ListofEmployees = csv.GetRecords<Employee>().ToList();
                }
            }
        }

        #region Public methods

        public bool ContainsEmployeeWithID(string idToCheck)
        {
            return ListofEmployees.Any(emp => emp.ID.Equals(idToCheck, StringComparison.OrdinalIgnoreCase));
        }

        public void csvLeaveUpdater()
        {
            if (File.Exists(csvFilePath))
            {
                using (var writer = new StreamWriter(csvFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(ListofEmployees);
                }
            }
        }

        #endregion

        #region Private methods

        private List<string> GetEmployeeNames()
        {
            return ListofEmployees.Select(emp => emp.Name).ToList();
        }

        private List<string> GetAdminNames()
        {
            return ListofEmployees.Where(emp => emp.CanReport).Select(emp => emp.Name).ToList();
        }

        private List<string> GetLeaveTypes()
        {
            return new List<string>() { "Earned Leave", "Casual Leave", "Personal Leave" };
        }

        #endregion

        //private Employee GetCurrentEmployee(string empName)
        //{
        //    return ListofEmployees.FirstOrDefault(emp => emp.Name.Equals(empName, StringComparison.OrdinalIgnoreCase));
        //}
    }


    public class LeaveLogger
    {

        #region Private variable

        private string logFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "log.txt");

        #endregion

        #region Public methods
        public void LogLeave(string message)
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now}:\r\n{message}\r\n");
            }
        }

        #endregion
    }

}
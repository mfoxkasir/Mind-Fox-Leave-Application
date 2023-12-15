using System;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Windows.Markup;

namespace Mind_Fox_data
{
    public class Employee : INotifyPropertyChanged
    {
        // Implement PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

    public class EmployeeList
    {
        public List<Employee> EmpList = new List<Employee>();

        private string csvFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "data.csv");
        public EmployeeList()
        {
            if (File.Exists(csvFilePath))
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    EmpList = csv.GetRecords<Employee>().ToList();
                }
            }
        }

        public bool ContainsEmployeeWithID(string idToCheck)
        {
            return EmpList.Any(emp => emp.ID.Equals(idToCheck, StringComparison.OrdinalIgnoreCase));
        }

        public void LeaveUpdater()
        {
            if (File.Exists(csvFilePath))
            {
                using (var writer = new StreamWriter(csvFilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(EmpList);
                }
            }
        }
        public List<string> GetEmployeeNames()
        {
            return EmpList.Select(emp => emp.Name).ToList();
        }

        public List<string> GetAdminNames()
        {
            return EmpList.Where(emp => emp.CanReport).Select(emp => emp.Name).ToList();
        }
    }
}

public class LeaveLogger
{
    private string logFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "log.txt");
    public void LogLeave(string message)
    {
        using (StreamWriter writer = File.AppendText(logFilePath))
        {
            writer.WriteLine($"{DateTime.Now}:\r\n{message}\r\n");
        }
    }
}
using System;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Mind_Fox_data
{
    public class Employee
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public bool CanReport { get; set; }
        public int EarnedLeave { get; set; }
        public int CasualLeave { get; set; }
        public int PersonalLeave { get; set; }
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

        public void EmployeeWriter()
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
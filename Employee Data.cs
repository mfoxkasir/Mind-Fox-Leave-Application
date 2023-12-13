using System;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;

namespace Mind_Fox_data
{

    public class Employee
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public bool CanReport { get; set; }
        public int EarnedLeave{ get; set; }
        public int CasualLeave { get; set; }
        public int PersonalLeave { get; set; }

        public Employee(string name, string Id, bool canReport, int EL, int CL, int PL)
        {
            Name = name;
            ID = Id;
            CanReport = canReport;
            EarnedLeave = EL;
            CasualLeave = CL;
            PersonalLeave = PL;
        }
    }

    public class EmployeeList : List<Employee>
    {
        public EmployeeList()
        {
            string csvFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "data.csv");
            if (File.Exists(csvFilePath))
            {
                using (var reader = new StreamReader("./data.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Employee>();
                }
            }
        }

        public bool ContainsEmployeeWithID(string nameToCheck)
        {
            return this.Any(employee => employee.ID.Equals(nameToCheck, StringComparison.OrdinalIgnoreCase));
        }
    }



}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BYT_Project
{
    

    public abstract class Staff
    {
        private string _name;
        private DateTime _employmentDate;
        private double _baseSalary;

        public static double YearlySalaryGrowthPercentage = 0.05;
    
        public static List<Staff> Extent = new List<Staff>();
    
    

        public string Name
        {
            get => _name;
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name can't be empty");
                }
                _name = value;
            }
        }

        public DateTime EmploymentDate
        {
            get => _employmentDate;
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentNullException("Employment Date can't be empty");
                }
                _employmentDate = value;
            }
        }

        public double Salary
        {
            get
            {
                int yearsSinceEmployment = EmploymentDate.Year - _employmentDate.Year;
                return _baseSalary * (1 + YearlySalaryGrowthPercentage * yearsSinceEmployment);
            }
        
        }

        public Staff(string name, DateTime employmentDate, double baseSalary)
        {
            _name = name;
            _employmentDate = employmentDate;
            _baseSalary = baseSalary;
        
            Extent.Add(this);
        }
        public static void SaveExtent(string fileName = "staff_extent.json")
        {
            var json = JsonSerializer.Serialize(
                Extent,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(fileName, json);
        }
        public static void LoadExtent(string fileName = "staff_extent.json") {
            if (!File.Exists(fileName))
                return;

            var json = File.ReadAllText(fileName);
            Extent = JsonSerializer.Deserialize<List<Staff>>(json);
        }
    }
}
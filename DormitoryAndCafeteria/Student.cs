using System;
using System.IO;
using System.Text.Json;

namespace DormitoryAndCafeteriaSystem
{
    public class Student : Person
    {
       

        public string Dormitory { get; set; }   // Konvikti
        public string Lastname { get; set; } = string.Empty;
        public decimal MonthlyFee { get; set; } 
        public Student(int id, string name, string lastname, string dormitory)
            : base(id, name)
        {
            Dormitory = dormitory;
             // default monthly fee
        }

        public decimal CalculateMonthlyCost()
        {
            return MonthlyFee;
        }

       
            public override string ToString()
        {
            return $"{Id} | {Name} {Lastname} | {Dormitory}";
        }

        

        // ---------------- JSON METHODS ----------------

        public void SaveToFile(string filename)
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);
        }

        public static Student? LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return null;
            string json = File.ReadAllText(filename);
            Student? student = JsonSerializer.Deserialize<Student>(json);
            return student;
        }
    }
}

using System;
using System.IO;
using System.Text.Json;

namespace DormitoryAndCafeteriaSystem
{
    public class Student : Person
    {
        public string Dormitory { get; set; } = string.Empty;
        public decimal MonthlyFee { get; set; } = 100;

        public Student() : base(0, string.Empty) { } // For JSON

        public Student(int id, string name, string dormitory)
            : base(id, name)
        {
            Dormitory = dormitory;
        }

        public override decimal CalculateMonthlyCost()
        {
            return MonthlyFee;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | Dormitory: {Dormitory} | Fee: {MonthlyFee}€";
        }

        public override string ToString() => GetInfo();

        public void SaveToFile(string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(fileName, JsonSerializer.Serialize(this, options));
        }

        public static Student LoadFromFile(string fileName)
        {
            return JsonSerializer.Deserialize<Student>(File.ReadAllText(fileName))!;
        }
    }
}

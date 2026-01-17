using System;
using System.IO;
using System.Text.Json;

namespace DormitoryAndCafeteriaSystem
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Dormitory { get; set; } = "";
        public string? AppliedDormitory { get; set; } = null;
        public decimal MonthlyLimit { get; set; } = 150;
        public decimal CurrentCafeteriaDebt { get; set; } = 0;
        public int? AssignedRoomNumber { get; set; } = null;
        public int? DormitoryID { get; set; }

        public bool CanOrder(decimal price)
        {
            return CurrentCafeteriaDebt + price <= MonthlyLimit;
        }
        


        public Student(int id, string name, string lastname, string dormitory)
        {
            Id = id;
            Name = name;
            LastName = lastname;
            Dormitory = dormitory;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} {LastName} | Dormitory: {Dormitory} | Applied: {AppliedDormitory ?? "None"}";
        }

    // Save & Load JSON mbetet siç është



        // ---------------- JSON METHODS ----------------

        public void SaveToFile(string filename)
        {
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);

            // Kjo tregon ne console ku u ruajt file
            Console.WriteLine($"Student data saved to: {Path.GetFullPath(filename)}");
        }


        public static Student? LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return null;
            string json = File.ReadAllText(filename);
            Student? student = JsonSerializer.Deserialize<Student>(json);
            return student;
        }
        public bool HasPaid { get; set; } = false;

        public decimal CalculateMonthlyCost()
        {
            return 150; // ose mund te llogaritet ne varesi te konviktit dhe kafeterise
        }
        public void ResetMonthlyData()
        {
            CurrentCafeteriaDebt = 0;
            HasPaid = false;
        }



    }
}

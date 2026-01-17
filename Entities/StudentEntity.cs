using System;

namespace DormitoryAndCafeteriaSystem.Entities
{
    public class Student
    {
        // ---------------- BASIC INFO ----------------
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        // ---------------- DORMITORY & ROOM ----------------
        public int? DormitoryId { get; set; }
        public int? AssignedRoomNumber { get; set; }
        public bool HasPaid { get; set; } = false;

        // ---------------- APPLICATION ----------------
        public string? AppliedDormitory { get; set; }  // kjo e rregullon gabimin

        // ---------------- CONSTRUCTOR ----------------
        public Student(int id, string name, string lastName, int? dormitoryId = null)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            DormitoryId = dormitoryId;
        }

        public Student() { }

        // ---------------- BUSINESS LOGIC ----------------
        public decimal CalculateMonthlyCost()
        {
            return 100m;
        }

        public override string ToString()
        {
            return $"{Id} | {Name} {LastName} | DormitoryID: {(DormitoryId?.ToString() ?? "N/A")} | Room: {(AssignedRoomNumber?.ToString() ?? "N/A")}";
        }
    }
}

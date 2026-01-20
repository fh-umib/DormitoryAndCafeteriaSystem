using System;
using System.Collections.Generic;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class RoomService
    {
        private readonly List<Student> assignedStudents = new();

        public void ShowDormRules()
        {
            Console.WriteLine("=== RULES OF THE BOARDING HOUSE ===");
            Console.WriteLine("1. Curfew: 23:00");
            Console.WriteLine("2. Smoking is prohibited in the rooms");
            Console.WriteLine("3. Visitors only until 21:00");
            Console.WriteLine("4. Monthly payment is mandatory");
            Console.WriteLine("5. Damage to property is punishable");
        }

        public void AssignRoomToStudent(Student student)
        {
            if (assignedStudents.Contains(student))
            {
                Console.WriteLine("Student already has a room.");
                return;
            }

            // Zgjidh dhomen e lirë manualisht ose automatikisht
            assignedStudents.Add(student);
            student.AssignedRoomNumber = new Random().Next(1, 50); // Për shembull dhoma e rastësishme
            Console.WriteLine($"Room {student.AssignedRoomNumber} assigned to {student.Name}.");
        }

        public decimal MonthlyFee(Student student) => 100m;
    }
}

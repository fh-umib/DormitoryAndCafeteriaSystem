using System;
using System.Collections.Generic;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem
{
    class DormitorySystem
    {
        public void RegisterStudent(List<Student> students)
        {
            // ---------------- ID ----------------
            int id;
            while (true)
            {
                Console.Write("ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id))
                    break;

                Console.WriteLine("Invalid ID. Try again.");
            }

            // ---------------- Name ----------------
            Console.Write("First Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine() ?? string.Empty;

            // ---------------- Dormitory ID (nullable int) ----------------
            int? dormId = null;
            Console.Write("Dormitory ID (optional): ");
            string? dormInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dormInput))
            {
                if (int.TryParse(dormInput, out int parsedDorm))
                    dormId = parsedDorm;
                else
                    Console.WriteLine("Invalid Dormitory ID. Will be left empty.");
            }

            // ---------------- Add Student ----------------
            students.Add(new Student(id, name, lastName, dormId));
            Console.WriteLine("Student registered successfully.");
        }

        public void ViewStudents(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }

            foreach (var s in students)
            {
                string dormDisplay = s.DormitoryId.HasValue ? s.DormitoryId.Value.ToString() : "None";
                Console.WriteLine($"ID: {s.Id}, Name: {s.Name} {s.LastName}, Dormitory ID: {dormDisplay}");
            }
        }

        public void PlaceOrder(List<Student> students, List<CafeteriaOrder> orders)
        {
            int id;
            while (true)
            {
                Console.Write("Student ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id))
                    break;

                Console.WriteLine("Invalid ID. Try again.");
            }

            var student = students.Find(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            Console.Write("Product: ");
            string product = Console.ReadLine() ?? string.Empty;

            decimal price;
            while (true)
            {
                Console.Write("Price: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && decimal.TryParse(input, out price))
                    break;

                Console.WriteLine("Invalid price. Try again.");
            }

            orders.Add(new CafeteriaOrder(product, price, id));
            Console.WriteLine("Order registered successfully.");
        }
    }
}

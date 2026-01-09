using System;
using System.Collections.Generic;

namespace DormitoryAndCafeteriaSystem
{
    class DormitorySystem
    {
        public void RegisterStudent(List<Student> students)
        {
            int id;
            while (true)
            {
                Console.Write("ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id))
                    break;

                Console.WriteLine("Invalid ID. Try again.");
            }

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Dormitory: ");
            string dormitory = Console.ReadLine() ?? string.Empty;

            Console.Write("Last Name: ");
            string lastname = Console.ReadLine() ?? string.Empty;

            students.Add(new Student(id, name, lastname, dormitory));
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
                Console.WriteLine(s);
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

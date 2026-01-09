using System;
using System.Collections.Generic;
using System.IO;

namespace DormitoryAndCafeteriaSystem
{
    class Program
    {
        static List<Student> students = new();
        static CafeteriaSystem cafeteria = new();

        static void Main()
        {
            LoadAllStudents();

            while (true)
            {
                Console.Clear();
                DisplayMenu();
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": 
                        RegisterStudent(); 
                        break;
                    case "2":
                        ViewAllStudents(); 
                        break;
                    case "3":
                        cafeteria.PlaceOrder(students); 
                        break;
                    case "4": cafeteria.ViewAllOrders(); 
                        break;
                    case "5": ViewOrdersByDormitory();
                        break;
                    case "6": ViewStudentMonthlyCost();
                        break;
                    case "7": RemoveStudent();
                        break;
                    case "8":
                        SaveAllData();
                        Console.WriteLine("Exiting... Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        Pause(); break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("====== DORMITORY & CAFETERIA SYSTEM ======");
            Console.WriteLine("1. Register New Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Place Cafeteria Order");
            Console.WriteLine("4. View All Orders");
            Console.WriteLine("5. View Orders By Dormitory");
            Console.WriteLine("6. View Student Monthly Cost");
            Console.WriteLine("7. Remove Student");
            Console.WriteLine("8. Exit");
            Console.WriteLine("==========================================\n");
            Console.Write("Enter choice: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // -------------------- CASE METHODS --------------------
        static void RegisterStudent()
        {
            int id;
            while (true)
            {
                Console.Write("ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id)) break;
                Console.WriteLine("Invalid input!");
            }

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Dormitory: ");
            string dormitory = Console.ReadLine() ?? "";

            var student = new Student(id, name, dormitory);
            students.Add(student);
            student.SaveToFile($"student_{id}.json");

            Console.WriteLine("Student registered successfully.");
            Pause();
        }

        static void ViewAllStudents()
        {
            if (students.Count == 0) Console.WriteLine("No students found.");
            else students.ForEach(s => Console.WriteLine(s));
            Pause();
        }

        static void ViewOrdersByDormitory()
        {
            Console.Write("Dormitory: ");
            string dorm = Console.ReadLine() ?? "";
            cafeteria.ViewOrdersByDormitory(students, dorm);
        }

        static void ViewStudentMonthlyCost()
        {
            int id;
            while (true)
            {
                Console.Write("Student ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id)) break;
                Console.WriteLine("Invalid input!");
            }

            var student = students.Find(s => s.Id == id);
            if (student == null) { Console.WriteLine("Student not found."); Pause(); return; }

            Console.WriteLine($"{student.Name} Monthly Cost: {student.CalculateMonthlyCost()}€");
            Console.WriteLine($"Total Spent in Cafeteria: {cafeteria.TotalSpentByStudent(id)}€");
            Pause();
        }

        static void RemoveStudent()
        {
            int id;
            while (true)
            {
                Console.Write("Student ID to remove: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id)) break;
                Console.WriteLine("Invalid input!");
            }

            students.RemoveAll(s => s.Id == id);
            cafeteria.RemoveOrdersByStudent(id);

            if (File.Exists($"student_{id}.json")) File.Delete($"student_{id}.json");

            Console.WriteLine("Student and related orders removed.");
            Pause();
        }

        static void LoadAllStudents()
        {
            foreach (var file in Directory.GetFiles(".", "student_*.json"))
            {
                try { students.Add(Student.LoadFromFile(file)); }
                catch { }
            }
        }

        static void SaveAllData()
        {
            foreach (var s in students) s.SaveToFile($"student_{s.Id}.json");
            cafeteria.SaveAllOrders();
        }
    }
}

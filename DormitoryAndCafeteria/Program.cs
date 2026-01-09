using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DormitoryAndCafeteriaSystem1;

namespace DormitoryAndCafeteriaSystem
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<CafeteriaOrder> orders = new List<CafeteriaOrder>();

        static void Main()
        {
            DormitorySystem system = new DormitorySystem();
            LoadData();

            while (true)
            {
                Console.WriteLine("\n1. Regjistro Student");
                Console.WriteLine("2. Shfaq Studentet");
                Console.WriteLine("3. Porosi Byfe");
                Console.WriteLine("4. Dil");
                Console.Write("Zgjedhja: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        system.RegisterStudent(students);
                        break;
                    case "2":
                        system.ViewStudents(students);
                        break;
                    case "3":
                        system.PlaceOrder(students, orders);
                        break;
                    case "4":
                        SaveData();
                        return;
                    default:
                        Console.WriteLine("Zgjedhje e pavlefshme!");
                        break;
                }
            }
        }

        static void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            File.WriteAllText("students.json",
                JsonSerializer.Serialize(students, options));

            File.WriteAllText("orders.json",
                JsonSerializer.Serialize(orders, options));
        }

        static void LoadData()
        {
            if (File.Exists("students.json"))
            {
                students = JsonSerializer.Deserialize<List<Student>>(
                    File.ReadAllText("students.json"));
            }

            if (File.Exists("orders.json"))
            {
                orders = JsonSerializer.Deserialize<List<CafeteriaOrder>>(
                    File.ReadAllText("orders.json"));
            }
        }
    }
}

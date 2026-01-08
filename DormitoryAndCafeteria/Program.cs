using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DormitoryAndCafeteriaSystem
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<CafeteriaOrder> orders = new List<CafeteriaOrder>();

        static void Main()
        {
            LoadData();

            while (true)
            {
                //Console.WriteLine("Miresevini!");
                Console.WriteLine("\n1. Regjistro Student");
                Console.WriteLine("2. Shfaq Studentet");
                Console.WriteLine("3. Porosi Byfe");
                Console.WriteLine("4. Dil");
                Console.Write("Zgjedhja: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterStudent();
                        break;

                    case "2":
                        ViewStudents();
                        break;

                    case "3":
                        PlaceOrder();
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

        static void RegisterStudent()
        {
            try
            {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Emri: ");
                string name = Console.ReadLine();

                Student s = new Student(id, name);
                students.Add(s);

                Console.WriteLine("Studenti u regjistrua me sukses.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ViewStudents()
        {
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }
        }

        static void PlaceOrder()
        {
            try
            {
                Console.Write("ID e studentit: ");
                int id = int.Parse(Console.ReadLine());

                Student s = students.Find(x => x.Id == id);
                if (s == null)
                    throw new Exception("Studenti nuk ekziston!");

                Console.Write("Produkti: ");
                string product = Console.ReadLine();

                Console.Write("Cmimi: ");
                decimal price = decimal.Parse(Console.ReadLine());

                CafeteriaOrder order = new CafeteriaOrder(product, price, id);
                orders.Add(order);

                Console.WriteLine("Porosia u regjistrua.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SaveData()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            File.WriteAllText("students.json",
                JsonSerializer.Serialize(students, options));

            File.WriteAllText("orders.json",
                JsonSerializer.Serialize(orders, options));

            Console.WriteLine("Te dhenat u ruajten.");
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

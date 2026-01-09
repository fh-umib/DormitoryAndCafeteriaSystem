using System;
using System.Collections.Generic;

namespace DormitoryAndCafeteriaSystem
{
    class DormitorySystem
    {
        public void RegisterStudent(List<Student> students)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Emri: ");
            string name = Console.ReadLine();

            students.Add(new Student(id, name));
            Console.WriteLine("Studenti u regjistrua me sukses.");
        }

        public void ViewStudents(List<Student> students)
        {
            foreach (var s in students)
                Console.WriteLine(s);
        }

        public void PlaceOrder(List<Student> students, List<CafeteriaOrder> orders)
        {
            Console.Write("ID e studentit: ");
            int id = int.Parse(Console.ReadLine());

            var student = students.Find(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Studenti nuk ekziston!");
                return;
            }

            Console.Write("Produkti: ");
            string product = Console.ReadLine();

            Console.Write("Cmimi: ");
            decimal price = decimal.Parse(Console.ReadLine());

            orders.Add(new CafeteriaOrder(product, price, id));
            Console.WriteLine("Porosia u regjistrua.");
        }
    }
}

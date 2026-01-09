using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DormitoryAndCafeteriaSystem
{
    public class CafeteriaSystem
    {
        public List<CafeteriaOrder> orders;

        public CafeteriaSystem()
        {
            orders = new List<CafeteriaOrder>();
            LoadAllOrders();
        }

        // -------------------- PLACE ORDER --------------------
        public void PlaceOrder(List<Student> students)
        {
            int id;
            while (true)
            {
                Console.Write("Student ID: ");
                if (int.TryParse(Console.ReadLine(), out id)) break;
                Console.WriteLine("Invalid input!");
            }

            var student = students.Find(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Pause();
                return;
            }

            Console.Write("Product: ");
            string product = Console.ReadLine() ?? "";

            decimal price;
            while (true)
            {
                Console.Write("Price: ");
                if (decimal.TryParse(Console.ReadLine(), out price)) break;
                Console.WriteLine("Invalid price!");
            }

            var order = new CafeteriaOrder(product, price, id);
            orders.Add(order);
            order.SaveToFile($"order_{id}_{DateTime.Now.Ticks}.json");

            Console.WriteLine("Order placed successfully.");
            Pause();
        }


        // -------------------- VIEW ORDERS --------------------
        public void ViewAllOrders()
        {
            if (orders.Count == 0)
                Console.WriteLine("No orders found.");
            else
                orders.ForEach(o => Console.WriteLine(o));
            Pause();
        }

        public void ViewOrdersByStudent(int studentId)
        {
            var studentOrders = orders.Where(o => o.StudentId == studentId).ToList();
            if (!studentOrders.Any())
                Console.WriteLine("No orders found for this student.");
            else
                studentOrders.ForEach(o => Console.WriteLine(o));
            Pause();
        }

        public void ViewOrdersByDormitory(List<Student> students, string dorm)
        {
            var dormIds = students.Where(s => s.Dormitory.Equals(dorm, StringComparison.OrdinalIgnoreCase))
                                  .Select(s => s.Id).ToList();
            var dormOrders = orders.Where(o => dormIds.Contains(o.StudentId)).ToList();

            if (!dormOrders.Any())
                Console.WriteLine("No orders found for this dormitory.");
            else
                dormOrders.ForEach(o => Console.WriteLine(o));
            Pause();
        }

        // -------------------- TOTALS --------------------
        public decimal TotalSpentByStudent(int studentId)
            => orders.Where(o => o.StudentId == studentId).Sum(o => o.Price);

        public decimal TotalSpentByDormitory(List<Student> students, string dorm)
        {
            var dormIds = students.Where(s => s.Dormitory.Equals(dorm, StringComparison.OrdinalIgnoreCase))
                                  .Select(s => s.Id).ToList();
            return orders.Where(o => dormIds.Contains(o.StudentId)).Sum(o => o.Price);
        }

        // -------------------- REMOVE ORDERS --------------------
        public void RemoveOrdersByStudent(int studentId)
        {
            orders.RemoveAll(o => o.StudentId == studentId);
            foreach (var file in Directory.GetFiles(".", $"order_{studentId}_*.json"))
                File.Delete(file);
        }

        // -------------------- LOAD / SAVE --------------------
        private void LoadAllOrders()
        {
            foreach (var file in Directory.GetFiles(".", "order_*.json"))
            {
                try
                {
                    orders.Add(CafeteriaOrder.LoadFromFile(file));
                }
                catch { }
            }
        }

        public void SaveAllOrders()
        {
            foreach (var o in orders)
                o.SaveToFile($"order_{o.StudentId}_{DateTime.Now.Ticks}.json");
        }

        // -------------------- UTILS --------------------
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

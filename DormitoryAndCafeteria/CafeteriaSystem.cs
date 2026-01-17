using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using DormitoryAndCafeteriaSystem.Data;

namespace DormitoryAndCafeteriaSystem
{
    public class CafeteriaSystem
    {
        // PLACE ORDER
        public void PlaceOrder(List<Student> students)
        {
            Console.Write("Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID!");
                Pause();
                return;
            }

            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Pause();
                return;
            }

            Console.Write("Product: ");
            string product = Console.ReadLine() ?? "";

            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price!");
                Pause();
                return;
            }

            // Kontrolli i limitit 150€
            decimal currentDebt = GetCurrentCafeteriaDebt(student.Id);
            if (currentDebt + price > 150)
            {
                Console.WriteLine("Nuk lejohet: e ke kalu limitin mujor 150€.");
                Console.WriteLine("Duhet me pagu CASH.");
                Pause();
                return;
            }

            try
            {
                

                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
    @"INSERT INTO CafeteriaOrderNew (studentid, product, price) 
      VALUES (@studentId, @product, @price);", conn);

                cmd.Parameters.AddWithValue("studentId", student.Id);
                cmd.Parameters.AddWithValue("product", product);
                cmd.Parameters.AddWithValue("price", price);

                cmd.ExecuteNonQuery();


                

                Console.WriteLine("Order placed successfully in the database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
            }

            Pause();
        }

        // GET CURRENT DEBT
        private decimal GetCurrentCafeteriaDebt(int studentId)
        {
            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"SELECT COALESCE(SUM(Price),0) FROM CafeteriaOrder
                      WHERE StudentID = @studentId AND Status='PLACED';", conn);

                cmd.Parameters.AddWithValue("studentId", studentId);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
            catch
            {
                return 0m;
            }
        }

        // ------------------ VIEW ALL ORDERS ------------------
        public void ViewAllOrders()
        {
            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"SELECT o.OrderID, o.StudentID, s.FirstName, s.LastName, o.Product, o.Price, o.OrderDate, o.Status
                      FROM CafeteriaOrder o
                      JOIN Student s ON o.StudentID = s.StudentID
                      ORDER BY o.OrderDate;", conn);

                using var reader = cmd.ExecuteReader();
                Console.WriteLine("\nAll Cafeteria Orders:");
                while (reader.Read())
                {
                    Console.WriteLine($"OrderID: {reader["OrderID"]}, Student: {reader["FirstName"]} {reader["LastName"]}, Product: {reader["Product"]}, Price: {reader["Price"]}€, Date: {reader["OrderDate"]}, Status: {reader["Status"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }

            Pause();
        }

        // ------------------ VIEW ORDERS BY DORM ------------------
        public void ViewOrdersByDormitory(List<Student> students, string dorm)
        {
            var dormIds = students
                .Where(s => s.Dormitory.Equals(dorm, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.Id)
                .ToList();

            if (dormIds.Count == 0)
            {
                Console.WriteLine("No students found for this dorm.");
                Pause();
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"SELECT o.OrderID, o.StudentID, s.FirstName, s.LastName, o.Product, o.Price, o.OrderDate, o.Status
                      FROM CafeteriaOrder o
                      JOIN Student s ON o.StudentID = s.StudentID
                      WHERE o.StudentID = ANY(@studentIds)
                      ORDER BY o.OrderDate;", conn);

                cmd.Parameters.AddWithValue("studentIds", dormIds.ToArray());
                using var reader = cmd.ExecuteReader();

                Console.WriteLine($"\nOrders for dormitory: {dorm}");
                bool found = false;
                while (reader.Read())
                {
                    found = true;
                    Console.WriteLine($"OrderID: {reader["OrderID"]}, Student: {reader["FirstName"]} {reader["LastName"]}, Product: {reader["Product"]}, Price: {reader["Price"]}€, Date: {reader["OrderDate"]}, Status: {reader["Status"]}");
                }

                if (!found)
                    Console.WriteLine("No orders found for this dormitory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }

            Pause();
        }

        // ------------------ TOTAL SPENT BY STUDENT ------------------
        public decimal TotalSpentByStudent(int studentId)
        {
            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"SELECT COALESCE(SUM(Price),0) FROM CafeteriaOrder
                      WHERE StudentID = @studentId AND Status='PLACED';", conn);

                cmd.Parameters.AddWithValue("studentId", studentId);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
            catch
            {
                return 0m;
            }
        }

        // ------------------ REMOVE ORDERS BY STUDENT ------------------
        public void RemoveOrdersByStudent(int studentId)
        {
            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"DELETE FROM CafeteriaOrder WHERE StudentID = @studentId;", conn);

                cmd.Parameters.AddWithValue("studentId", studentId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing orders: {ex.Message}");
            }
        }

        // ------------------ PAUSE ------------------
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

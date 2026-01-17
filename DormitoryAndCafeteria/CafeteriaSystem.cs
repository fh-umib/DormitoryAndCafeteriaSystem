using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using DormitoryAndCafeteriaSystem.Data;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem
{
    public class CafeteriaSystem
    {
        // ------------------ PLACE ORDER ------------------
        public void PlaceOrder(List<Student> students)
        {
            Console.Write("Enter Student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Invalid student ID!");
                Pause();
                return;
            }

            var student = students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found!");
                Pause();
                return;
            }

            Console.Write("Product: ");
            string product = Console.ReadLine() ?? string.Empty;

            Console.Write("Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price!");
                Pause();
                return;
            }

            // Kontrolli i limitit 150€
            decimal currentDebt = GetCurrentCafeteriaDebt(studentId);
            if (currentDebt + price > 150)
            {
                Console.WriteLine("Nuk lejohet: e ke kaluar limitin mujor 150€.");
                Console.WriteLine("Pagesa duhet me u bo CASH.");
                Pause();
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"INSERT INTO CafeteriaOrderNew (StudentID, Product, Price, Status)
                      VALUES (@studentId, @product, @price, 'PLACED');", conn);

                cmd.Parameters.AddWithValue("studentId", studentId);
                cmd.Parameters.AddWithValue("product", product);
                cmd.Parameters.AddWithValue("price", price);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Order placed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
            }

            Pause();
        }

        // ------------------ GET CURRENT DEBT ------------------
        private decimal GetCurrentCafeteriaDebt(int studentId)
        {
            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"SELECT COALESCE(SUM(Price), 0)
                      FROM CafeteriaOrderNew
                      WHERE StudentID = @studentId
                        AND Status = 'PLACED';", conn);

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
                    @"SELECT o.OrderID,
                             o.StudentID,
                             s.FirstName,
                             s.LastName,
                             o.Product,
                             o.Price,
                             o.OrderDate,
                             o.Status
                      FROM CafeteriaOrderNew o
                      JOIN Student s ON s.StudentID = o.StudentID
                      ORDER BY o.OrderDate;", conn);

                using var reader = cmd.ExecuteReader();
                Console.WriteLine("\nAll Cafeteria Orders:");

                while (reader.Read())
                {
                    Console.WriteLine(
                        $"OrderID: {reader["OrderID"]}, " +
                        $"Student: {reader["FirstName"]} {reader["LastName"]}, " +
                        $"Product: {reader["Product"]}, " +
                        $"Price: {reader["Price"]}€, " +
                        $"Date: {reader["OrderDate"]}, " +
                        $"Status: {reader["Status"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }

            Pause();
        }

        // ------------------ VIEW ORDERS BY DORMITORY ------------------
        public void ViewOrdersByDormitory(List<Student> students, int dormId)
        {
            var studentIds = students
                .Where(s => s.DormitoryId.HasValue && s.DormitoryId.Value == dormId)
                .Select(s => s.Id)
                .ToArray();

            if (studentIds.Length == 0)
            {
                Console.WriteLine("No students found for this dormitory.");
                Pause();
                return;
            }

            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"SELECT o.OrderID,
                             o.StudentID,
                             s.FirstName,
                             s.LastName,
                             o.Product,
                             o.Price,
                             o.OrderDate,
                             o.Status
                      FROM CafeteriaOrderNew o
                      JOIN Student s ON s.StudentID = o.StudentID
                      WHERE o.StudentID = ANY(@studentIds)
                      ORDER BY o.OrderDate;", conn);

                cmd.Parameters.AddWithValue("studentIds", studentIds);

                using var reader = cmd.ExecuteReader();
                Console.WriteLine($"\nOrders for Dormitory ID: {dormId}");

                bool found = false;
                while (reader.Read())
                {
                    found = true;
                    Console.WriteLine(
                        $"OrderID: {reader["OrderID"]}, " +
                        $"Student: {reader["FirstName"]} {reader["LastName"]}, " +
                        $"Product: {reader["Product"]}, " +
                        $"Price: {reader["Price"]}€, " +
                        $"Date: {reader["OrderDate"]}, " +
                        $"Status: {reader["Status"]}");
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
            return GetCurrentCafeteriaDebt(studentId);
        }

        // ------------------ REMOVE ORDERS BY STUDENT ------------------
        public void RemoveOrdersByStudent(int studentId)
        {
            try
            {
                using var conn = DbConnectionFactory.Create();
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    @"DELETE FROM CafeteriaOrderNew
                      WHERE StudentID = @studentId;", conn);

                cmd.Parameters.AddWithValue("studentId", studentId);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Orders removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing orders: {ex.Message}");
            }

            Pause();
        }

        // ------------------ PAUSE ------------------
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

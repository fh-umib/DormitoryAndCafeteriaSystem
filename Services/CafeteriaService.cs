using Npgsql;
using DormitoryAndCafeteriaSystem.Data;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class CafeteriaService
    {
        public void LoadAllOrders() { }

        public void PlaceOrder(Student student)
        {
            Console.Write("Product: ");
            string product = Console.ReadLine()!;

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine()!);

            decimal current = TotalSpentByStudent(student.Id);
            if (current + price > 150)
            {
                Console.WriteLine("Monthly cafeteria limit exceeded.");
                return;
            }

            using var conn = DbConnectionFactory.Create();
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"INSERT INTO CafeteriaOrderNEW(StudentID, Product, Price, OrderDate, Status)
                  VALUES (@s,@p,@pr,NOW(),'PLACED')", conn);


            cmd.Parameters.AddWithValue("s", student.Id);
            cmd.Parameters.AddWithValue("p", product);
            cmd.Parameters.AddWithValue("pr", price);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Order placed successfully.");
        }

        public decimal TotalSpentByStudent(int studentId)
        {
            using var conn = DbConnectionFactory.Create();
            conn.Open();

            using var cmd = new NpgsqlCommand(
                @"SELECT COALESCE(SUM(Price),0)
                  FROM CafeteriaOrderNew
                  WHERE StudentID=@id AND Status='PLACED'", conn);

            cmd.Parameters.AddWithValue("id", studentId);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        public void ViewAllOrders() { }
        public void ViewOrdersByDormitory(List<Student> s, string d) { }
    }
}

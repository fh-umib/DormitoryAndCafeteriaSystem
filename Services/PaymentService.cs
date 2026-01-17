using System;
using Npgsql;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class PaymentService
    {
        private readonly string connectionString = "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;";

        // 🟢 Paguaj pagesën mujore për një student
        public void PayMonthlyFee(Student student, decimal amount)
        {
            if (student == null)
            {
                Console.WriteLine("Invalid student!");
                return;
            }

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                // 1️⃣ Shto pagesën në tabelën e pagesave (nëse ke një tabelë për pagesa)
                using (var cmdInsert = new NpgsqlCommand(
                    "INSERT INTO MonthlyPayments (StudentID, Amount, PaymentDate) VALUES (@id, @amount, CURRENT_DATE);", conn))
                {
                    cmdInsert.Parameters.AddWithValue("id", student.Id);
                    cmdInsert.Parameters.AddWithValue("amount", amount);
                    cmdInsert.ExecuteNonQuery();
                }

                // 2️⃣ Update totalin mujore të studentit në tabelën Student (opsional)
                using (var cmdUpdate = new NpgsqlCommand(
                    "UPDATE Student SET MonthlyFeePaid = COALESCE(MonthlyFeePaid,0) + @amount WHERE StudentID = @id;", conn))
                {
                    cmdUpdate.Parameters.AddWithValue("id", student.Id);
                    cmdUpdate.Parameters.AddWithValue("amount", amount);
                    cmdUpdate.ExecuteNonQuery();
                }

                Console.WriteLine($"Monthly fee of {amount}€ has been paid for {student.Name}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing payment: {ex.Message}");
            }
        }

        // 🟢 Reset pagesat mujore (p.sh. fillimi i muajit të ri)
        public void ResetMonthlyPayments()
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand("UPDATE Student SET MonthlyFeePaid = 0;", conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Monthly payments reset for all students.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resetting payments: {ex.Message}");
            }
        }

        // 🟢 Shiko pagesat mujore të një student
        public decimal GetMonthlyPaid(Student student)
        {
            if (student == null) return 0m;

            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using var cmd = new NpgsqlCommand("SELECT COALESCE(MonthlyFeePaid,0) FROM Student WHERE StudentID = @id;", conn);
                cmd.Parameters.AddWithValue("id", student.Id);

                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0m;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching monthly payment: {ex.Message}");
                return 0m;
            }
        }
    }
}

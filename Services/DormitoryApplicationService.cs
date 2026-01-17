
using Npgsql;
using System;
using DormitoryAndCafeteriaSystem.Entities; 


namespace DormitoryAndCafeteriaSystem.Services
{
    public class DormitoryApplicationService
    {
        /// <summary>
        /// Aplikon një student për një konvikt të caktuar.
        /// Ruhet edhe në memory (objekti Student) edhe në databazë.
        /// </summary>
        /// <param name="student">Objekti student në memory</param>
        /// <param name="dormId">ID e konviktit ku student aplikon</param>
        public void Apply(Student student, int dormId)
        {
            if (student == null)
            {
                Console.WriteLine("Student is null!");
                return;
            }

            // Ruaj në objektin në memory
            student.AppliedDormitory = dormId.ToString();

            // ------------------ SQL QUERY ------------------
            try
            {
                using var conn = new NpgsqlConnection(
                    "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;");
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "UPDATE Student SET AppliedDormitory = @dorm WHERE StudentID = @id;", conn);

                cmd.Parameters.AddWithValue("id", student.Id);
                cmd.Parameters.AddWithValue("dorm", dormId); // tipi int në DB

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"The dormitory application was accepted for {student.Name} to dorm {dormId}.");
                }
                else
                {
                    Console.WriteLine("Student not found in database. Application not saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving application to DB: {ex.Message}");
            }
            // -------------------------------------------------
        }
    }
}

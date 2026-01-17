using System.Collections.Generic;
using Npgsql;
using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Repositories.Interfaces;

namespace DormitoryAndCafeteriaSystem.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString =
             "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;";


        public void Add(Student student)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(
                "INSERT INTO Student (StudentID, FirstName, LastName, Email, Phone, DormitoryID) " +
                "VALUES (@id,@fn,@ln,@em,@ph,@dorm)", conn);

            cmd.Parameters.AddWithValue("id", student.Id);
            cmd.Parameters.AddWithValue("fn", student.Name);
            cmd.Parameters.AddWithValue("ln", student.LastName);
            cmd.Parameters.AddWithValue("em", student.Email ?? "");
            cmd.Parameters.AddWithValue("ph", student.Phone ?? "");
            cmd.Parameters.AddWithValue("dorm", (object?)student.DormitoryId ?? DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Remove(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand("DELETE FROM Student WHERE StudentID=@id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        public Student? GetById(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(
                "SELECT StudentID, FirstName, LastName, Email, Phone, DormitoryID FROM Student WHERE StudentID=@id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Student(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.IsDBNull(5) ? null : reader.GetInt32(5)
            )
            {
                Email = reader.GetString(3),
                Phone = reader.GetString(4)
            };
        }

        public List<Student> GetAll()
        {
            var list = new List<Student>();
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(
                "SELECT StudentID, FirstName, LastName, Email, Phone, DormitoryID FROM Student", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Student(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.IsDBNull(5) ? null : reader.GetInt32(5)
                )
                {
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4)
                });
            }
            return list;
        }

        public void Update(Student student)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            var cmd = new NpgsqlCommand(
                "UPDATE Student SET FirstName=@fn, LastName=@ln, Email=@em, Phone=@ph, DormitoryID=@dorm WHERE StudentID=@id", conn);

            cmd.Parameters.AddWithValue("fn", student.Name);
            cmd.Parameters.AddWithValue("ln", student.LastName);
            cmd.Parameters.AddWithValue("em", student.Email ?? "");
            cmd.Parameters.AddWithValue("ph", student.Phone ?? "");
            cmd.Parameters.AddWithValue("dorm", (object?)student.DormitoryId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("id", student.Id);

            cmd.ExecuteNonQuery();
        }
    }
}

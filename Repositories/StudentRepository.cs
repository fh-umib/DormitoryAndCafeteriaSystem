using System.Collections.Generic;
using Npgsql;
using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Data;

namespace DormitoryAndCafeteriaSystem.Repositories
{
    public class StudentRepository
    {
        public List<StudentEntity> GetAll()
        {
            var students = new List<StudentEntity>();

            using var conn = DbConnectionFactory.Create();
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM Student", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new StudentEntity
                {
                    StudentID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    DormitoryID = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                });
            }

            return students;
        }
    }
}

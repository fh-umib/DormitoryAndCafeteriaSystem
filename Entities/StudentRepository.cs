using Npgsql;
using DormitoryAndCafeteriaSystem.Data;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Repositories
{
    public class StudentRepository
    {
        public void Insert(StudentEntity s)
        {
            using var conn = DbConnectionFactory.Create();
            conn.Open();

            var cmd = new NpgsqlCommand("""
                INSERT INTO student(studentid, firstname, lastname, dormitoryid)
                VALUES (@id,@fn,@ln,@d)
            """, conn);

            cmd.Parameters.AddWithValue("@id", s.StudentID);
            cmd.Parameters.AddWithValue("@fn", s.FirstName);
            cmd.Parameters.AddWithValue("@ln", s.LastName);
            cmd.Parameters.AddWithValue("@d", s.DormitoryID);

            cmd.ExecuteNonQuery();
        }
    }
}


using Npgsql;

namespace DormitoryAndCafeteriaSystem.Services
{
    public static class DatabaseTester
    {
        public static void TestDatabase()

        {
            try
            {
                using var conn = new NpgsqlConnection(
                   "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;"
                );
                conn.Open();
                Console.WriteLine("Lidhja me databazën u realizua me sukses!");

                // Testo numrin e studentëve
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM student;", conn))
                {
                    var count = cmd.ExecuteScalar();
                    Console.WriteLine($"Numri i studentëve: {count}");
                }

                // Testo view (kujdes: view duhet të jetë me lowercase në PostgreSQL)
                using (var cmd = new NpgsqlCommand("SELECT * FROM vw_studentdormitoryinfo LIMIT 5;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("\nParaqitja e disa studentëve me dormitor:");
                    while (reader.Read())
                    {
                        string firstName = reader["firstname"].ToString() ?? "";
                        string lastName = reader["lastname"].ToString() ?? "";
                        string dormitoryName = reader["dormitoryname"].ToString() ?? "";
                        string roomNumber = reader["roomnumber"].ToString() ?? "";

                        Console.WriteLine($"{firstName} {lastName} - Dormitory: {dormitoryName} - Room: {roomNumber}");
                    }
                }

                Console.WriteLine("\n✅ View dhe query funksionojnë normalisht!");
            }
            catch (Npgsql.PostgresException pgEx)
            {
                Console.WriteLine($"⚠ PostgreSQL error: {pgEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Gabim gjatë lidhjes ose query: {ex.Message}");
            }
        }
    }
}

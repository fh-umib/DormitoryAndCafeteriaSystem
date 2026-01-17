using Npgsql;

namespace DormitoryAndCafeteriaSystem.Data
{
    public static class DBConnection
    {
        private static readonly string _connectionString =
           "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;";

        public static NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}

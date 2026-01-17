using Npgsql;

namespace DormitoryAndCafeteriaSystem.Data
{
    public static class DbConnectionFactory
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;";

        public static NpgsqlConnection Create()
        {
            return new NpgsqlConnection(ConnectionString);
        }
    }
}

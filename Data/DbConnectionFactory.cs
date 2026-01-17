using Npgsql;

namespace DormitoryAndCafeteriaSystem.Data
{
    public static class DbConnectionFactory
    {
        private static readonly string _cs =
            "Host=localhost;" +
            "Port=5432;" +
            "Database=DormitoryCafeteria;" +
            "Username=postgres;" +
            "Password=2206";

        public static NpgsqlConnection Create()
            => new NpgsqlConnection(_cs);
    }
}

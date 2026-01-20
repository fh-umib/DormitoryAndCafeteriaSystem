using Npgsql;
using System;

namespace DormitoryAndCafeteriaSystem.Data
{
    public static class DatabaseTester
    {
        public static void TestDatabase()
        {
            try
            {
                // Ketu perdorim DbConnectionFactory
                using var conn = DbConnectionFactory.Create();
                conn.Open();
                Console.WriteLine("Database connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database connection failed:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}

//using Npgsql;

//namespace DormitoryAndCafeteriaSystem.Services
//{
//    public static class DatabaseTester
//    {
//        public static void TestDatabase()
//        {
//            try
//            {
//                using var conn = new NpgsqlConnection(
//                    "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;"
//                );
//                conn.Open();
//                // ... pjesa tjetër e testimit
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Gabim gjatë lidhjes: {ex.Message}");
//            }
//        }
//    }
//}

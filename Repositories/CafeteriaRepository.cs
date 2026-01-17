using System.Collections.Generic;
using Npgsql;
using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Data;

namespace DormitoryAndCafeteriaSystem.Repositories
{
    public class CafeteriaRepository
    {
        public List<CafeteriaOrderEntity> GetOrders()
        {
            var orders = new List<CafeteriaOrderEntity>();

            using var conn = DbConnectionFactory.Create();
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM CafeteriaOrder", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(new CafeteriaOrderEntity
                {
                    OrderID = reader.GetInt32(0),
                    StudentID = reader.GetInt32(1),
                    CafeteriaID = reader.GetInt32(2),
                    OrderDate = reader.GetDateTime(3),
                    TotalAmount = reader.GetDecimal(4),
                    Status = reader.GetString(5)
                });
            }

            return orders;
        }
    }
}

using System.Collections.Generic;
using Npgsql;
using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Data;

namespace DormitoryAndCafeteriaSystem.Repositories
{
    public class RoomRepository
    {
        public List<RoomEntity> GetAll()
        {
            var rooms = new List<RoomEntity>();

            using var conn = DbConnectionFactory.Create();
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT * FROM Room", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new RoomEntity
                {
                    RoomID = reader.GetInt32(0),
                    DormitoryID = reader.GetInt32(1),
                    RoomNumber = reader.GetString(2),
                    Capacity = reader.GetInt32(3),
                    Type = reader.GetString(4)
                });
            }

            return rooms;
        }
    }
}

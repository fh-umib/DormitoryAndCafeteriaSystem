using System;
using System.Text.Json;
using System.IO;

namespace DormitoryAndCafeteriaSystem.Entities
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int Occupied { get; set; }

        public Room(int number, int capacity)
        {
            RoomNumber = number;
            Capacity = capacity;
            Occupied = 0;
        }

        public bool IsAvailable() => Occupied < Capacity;

        public void AssignStudent()
        {
            if (IsAvailable())
                Occupied++;
            else
                Console.WriteLine("Room is full! Choose another room.");
        }

        public void Save(string file)
        {
            File.WriteAllText(file, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static Room Load(string file)
        {
            return JsonSerializer.Deserialize<Room>(File.ReadAllText(file))!;
        }
    }
}

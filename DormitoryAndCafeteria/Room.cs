using System;

namespace DormitoryAndCafeteriaSystem
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public int Capacity { get; set; }
        public int OccupiedBeds { get; private set; }

        public Room(int roomNumber, int capacity)
        {
            RoomNumber = roomNumber;
            Capacity = capacity;
            OccupiedBeds = 0;
        }

        // Kontrollon a ka vend te lire (Business Logic)
        public bool IsAvailable()
        {
            return OccupiedBeds < Capacity;
        }

        // Shton nje student ne dhome
        public void AssignStudent()
        {
            if (!IsAvailable())
                throw new Exception("Dhoma eshte e mbushur!");

            OccupiedBeds++;
        }

        // Largon nje student nga dhoma
        public void RemoveStudent()
        {
            if (OccupiedBeds <= 0)
                throw new Exception("Dhoma eshte bosh!");

            OccupiedBeds--;
        }

        public override string ToString()
        {
            return $"Dhoma {RoomNumber} | Kapaciteti: {Capacity} | Te zena: {OccupiedBeds}";
        }
    }
}

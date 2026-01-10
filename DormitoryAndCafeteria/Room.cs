namespace DormitoryAndCafeteriaSystem
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

        public bool IsAvailable()
        {
            return Occupied < Capacity;
        }

        public void AssignStudent()
        {
            if (IsAvailable())
            {
                Occupied++;
            }
            else
            {
                Console.WriteLine("Room is full!");
            }
        }
    }
}

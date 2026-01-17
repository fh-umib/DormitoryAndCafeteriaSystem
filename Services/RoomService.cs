using DormitoryAndCafeteriaSystem.Repositories;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class RoomService
    {
        private readonly RoomRepository _repo = new();

        public void PrintRooms()
        {
            var rooms = _repo.GetAll();
            foreach (var r in rooms)
            {
                System.Console.WriteLine($"Room {r.RoomNumber} | Capacity: {r.Capacity}");
            }
        }
    }
}

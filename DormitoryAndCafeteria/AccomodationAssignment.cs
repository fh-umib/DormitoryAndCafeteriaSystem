using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DormitoryAndCafeteriaSystem
{

    public class AccomodationAssignment
    {
        public void AssignRoom(Student student, List<Room> rooms)
        {
            Console.WriteLine("Available rooms:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room {room.RoomNumber} - {room.Occupied}/{room.Capacity} occupied");
            }

            int roomNumber;
            while (true)
            {
                Console.Write("Enter the room number you want to apply for: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out roomNumber) && rooms.Any(r => r.RoomNumber == roomNumber))
                {
                    var chosenRoom = rooms.First(r => r.RoomNumber == roomNumber);
                    if (chosenRoom.IsAvailable())
                    {
                        chosenRoom.AssignStudent();
                        Console.WriteLine($"Student {student.Name} was assigned to room {chosenRoom.RoomNumber}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, that room is full. Choose another one.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid room number.");
                }
            }
        }
    }

}
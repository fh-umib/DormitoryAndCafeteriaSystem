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
            //  NËSE STUDENTI KA DHOMË
            if (student.AssignedRoomNumber != null)
            {
                Console.WriteLine(
                    $"Student {student.Name} is already assigned to room {student.AssignedRoomNumber}."
                );
                Console.WriteLine("Checkout from current room first.");
                return;
            }

            Console.WriteLine("Available rooms:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"Room {room.RoomNumber} - {room.Occupied}/{room.Capacity}");
            }

            Console.Write("Enter room number: ");
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            var chosenRoom = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (chosenRoom == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            if (!chosenRoom.IsAvailable())
            {
                Console.WriteLine("Room is full.");
                return;
            }

            // ASSIGN
            chosenRoom.AssignStudent();
            student.AssignedRoomNumber = chosenRoom.RoomNumber;

            Console.WriteLine(
                $"Student {student.Name} assigned to room {chosenRoom.RoomNumber}"
            );
        }

        public void Checkout(Student student, List<Room> rooms)
        {
            if (student.AssignedRoomNumber == null)
            {
                Console.WriteLine("Student is not assigned to any room.");
                return;
            }

            var room = rooms.FirstOrDefault(r => r.RoomNumber == student.AssignedRoomNumber);
            if (room != null && room.Occupied > 0)
            {
                room.Occupied--;
            }

            Console.WriteLine(
                $"Student {student.Name} checked out from room {student.AssignedRoomNumber}"
            );

            student.AssignedRoomNumber = null;
        }

    }

}
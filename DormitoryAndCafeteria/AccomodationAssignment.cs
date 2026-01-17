using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DormitoryAndCafeteriaSystem.Entities; 



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
                Console.WriteLine($"Student {student.Name} is not assigned to any room.");
                return;
            }

            int roomNumber = student.AssignedRoomNumber.Value;

            var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null)
            {
                Console.WriteLine("Room data not found.");
                return;
            }

            Console.WriteLine("===== CHECKOUT INFO =====");
            Console.WriteLine($"Student: {student.Name} {student.LastName}");
            Console.WriteLine($"Currently assigned room: {room.RoomNumber}");
            Console.WriteLine($"Occupancy before checkout: {room.Occupied}/{room.Capacity}");
            Console.WriteLine("=========================");

            Console.Write("Confirm checkout? (y/n): ");
            string? confirm = Console.ReadLine();

            if (confirm?.ToLower() != "y")
            {
                Console.WriteLine("Checkout cancelled.");
                return;
            }

            // CHECKOUT
            room.Occupied--;
            student.AssignedRoomNumber = null;

            Console.WriteLine("Checkout completed successfully.");
            Console.WriteLine($"Room {room.RoomNumber} occupancy is now {room.Occupied}/{room.Capacity}");
        }


    }

}
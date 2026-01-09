using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DormitoryAndCafeteriaSystem
    {
      
    public class AccomodationAssignment

        {
            public void AssignRoom(Student student, Room room)
            {
                if (!room.IsAvailable())
                {
                Console.WriteLine("Sorry, the room is full.");
                return;
                }

                room.AssignStudent();
                Console.WriteLine($"Studenti {student.Name} u caktua në dhomën {room.RoomNumber}");
            }
        }
    }

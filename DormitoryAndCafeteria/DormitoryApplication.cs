using System;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem
{
    public class DormitoryApplication
    {
        public void Apply(Student student)
        {
            Console.Write("Enter the dormitory you want to apply for (ID): ");
            int dormId;
            while (!int.TryParse(Console.ReadLine(), out dormId))
            {
                Console.Write("Please enter a valid dormitory ID: ");
            }

            student.AppliedDormitoryId = dormId;

            Console.WriteLine($"The dormitory application was accepted for {student.Name} to dorm {dormId}");
        }
    }
}

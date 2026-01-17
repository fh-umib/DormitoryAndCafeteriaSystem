using System;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem
{
    public class DormitoryApplication
    {
        public void Apply(Student student)
        {
            Console.Write("Enter the dormitory you want to apply for: ");
            string dorm = Console.ReadLine() ?? "";

            student.AppliedDormitory = dorm;

            Console.WriteLine($"The dormitory application was accepted for {student.Name} to {dorm}");
        }
    }
}

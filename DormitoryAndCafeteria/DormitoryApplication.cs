using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryAndCafeteriaSystem
{
    class DormitoryApplication
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


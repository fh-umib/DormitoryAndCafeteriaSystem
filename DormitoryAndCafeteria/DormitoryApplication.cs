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
            Console.WriteLine($"The dormitory application was accepted for the student {student.Name}");
        }
    }
}


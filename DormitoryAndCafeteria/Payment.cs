
using System;
using DormitoryAndCafeteriaSystem.Entities; // <--- kjo mungonte
namespace DormitoryAndCafeteriaSystem
{
    class Payment
    {
        public void PayMonthlyFee(Student student)
        {
            if (student.HasPaid)
            {
                Console.WriteLine($"{student.Name} has already paid for this month.");
            }
            else
            {
                Console.WriteLine($"{student.Name} paid {student.CalculateMonthlyCost()}€ for the current month.");
                student.HasPaid = true;
            }
        }
    }

}

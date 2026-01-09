using System;

namespace DormitoryAndCafeteriaSystem
{
    public class Student : Person
    {
        public decimal MonthlyFee { get; set; }

        public Student(int id, string name)
            : base(id, name)
        {
            MonthlyFee = 100;
        }

        public override decimal CalculateMonthlyCost()
        {
            return MonthlyFee;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | Student | Pagesa: {MonthlyFee}€";
        }
    }
}

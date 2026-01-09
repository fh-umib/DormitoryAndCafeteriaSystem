namespace DormitoryAndCafeteriaSystem
{
    class Payment
    {
        public void PayMonthlyFee(Student student)
        {
            Console.WriteLine($"{student.Name} pagoi {student.CalculateMonthlyCost()}€ për muajin aktual.");
        }
    }
}

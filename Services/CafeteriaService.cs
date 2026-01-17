using DormitoryAndCafeteriaSystem.Repositories;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class CafeteriaService
    {
        private readonly CafeteriaRepository _repo = new();

        public void PrintOrders()
        {
            var orders = _repo.GetOrders();
            foreach (var o in orders)
            {
                System.Console.WriteLine($"Order #{o.OrderID} | Total: {o.TotalAmount} | {o.Status}");
            }
        }
    }
}

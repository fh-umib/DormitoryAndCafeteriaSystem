using System;

namespace DormitoryAndCafeteriaSystem.Entities
{
    public class CafeteriaOrderEntity
    {
        public int OrderID { get; set; }
        public int StudentID { get; set; }
        public int CafeteriaID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
    }
}

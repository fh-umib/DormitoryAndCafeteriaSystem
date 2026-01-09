using System;

namespace DormitoryAndCafeteriaSystem
{
    public class CafeteriaOrder
    {
        public string Product { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public int PersonId { get; set; }

        public CafeteriaOrder(string product, decimal price, int personId)
        {
            Product = product;
            Price = price;
            PersonId = personId;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Date:dd.MM.yyyy HH:mm} | {Product} | {Price}€ | PersonID: {PersonId}";
        }
    }
}

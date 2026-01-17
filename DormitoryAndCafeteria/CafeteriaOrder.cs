using System;
using System.IO;
using System.Text.Json;

namespace DormitoryAndCafeteriaSystem
{
    public class CafeteriaOrder
    {
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }

        public CafeteriaOrder() { } 

        public CafeteriaOrder(string product, decimal price, int studentId)
        {
            Product = product ?? string.Empty;
            Price = price;
            StudentId = studentId;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Date:dd.MM.yyyy HH:mm} | {Product} | {Price}€ | StudentID: {StudentId}";
        }

        //public void SaveToFile(string fileName)
        //{
        //    var options = new JsonSerializerOptions { WriteIndented = true };
        //    File.WriteAllText(fileName, JsonSerializer.Serialize(this, options));
        //}

        //public static CafeteriaOrder LoadFromFile(string fileName)
        //{
        //    return JsonSerializer.Deserialize<CafeteriaOrder>(File.ReadAllText(fileName))!;
        //}
    }
}

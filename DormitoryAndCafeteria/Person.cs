using System;

namespace DormitoryAndCafeteriaSystem
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public virtual string GetInfo()
        {
            return $"ID: {Id} | Name: {Name}";
        }

        // Kjo metodë është virtuale, për të cilën mund të bëhet override
        public virtual decimal CalculateMonthlyCost()
        {
            return 0; // default = 0, mund të ndryshohet në Student apo Staff
        }
    }
}

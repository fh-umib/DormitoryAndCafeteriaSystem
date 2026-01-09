using System;

namespace DormitoryAndCafeteriaSystem
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Person() { }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public virtual decimal CalculateMonthlyCost() => 0;

        public virtual string GetInfo() => $"{Id} | {Name}";
    }

}




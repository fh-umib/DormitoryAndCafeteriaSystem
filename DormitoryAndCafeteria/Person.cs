using System;

namespace DormitoryAndCafeteriaSystem
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        protected Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        protected Person() { } // Required for JSON deserialization

        public abstract decimal CalculateMonthlyCost();

        public virtual string GetInfo()
        {
            return $"{Id} | {Name}";
        }
    }
}

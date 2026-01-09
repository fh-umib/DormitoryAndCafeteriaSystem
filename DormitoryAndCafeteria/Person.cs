using System;

namespace DormitoryAndCafeteriaSystem
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract decimal CalculateMonthlyCost();

        public virtual string GetInfo()
        {
            return $"ID: {Id} | Emri: {Name}";
        }

        public override string ToString()
        {
            return GetInfo();
        }
    }
}

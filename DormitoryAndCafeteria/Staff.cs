using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryAndCafeteriaSystem
{
    public class Staff : Person
    {
        public Staff(int id, string name)
            : base(id, name) { }

        public override decimal CalculateMonthlyCost()
        {
            return 0; // stafi nuk paguan
        }

        public override string GetInfo()
        {
            return base.GetInfo() + " | Staff";
        }
    }
}

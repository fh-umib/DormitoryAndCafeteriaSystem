namespace DormitoryAndCafeteriaSystem.Entities
{
    public class Student
    {
        // Identiteti
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        // Informacion për konvikt dhe dhomë
        public int? DormitoryId { get; set; }           // Dormitory ku studenti është assignuar
        public int? AppliedDormitoryId { get; set; }    // Dormitory ku studenti ka aplikuar
        public int? AssignedRoomNumber { get; set; }   // Numri i dhomës ku është assignuar

        // Të dhëna shtesë
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Pagesa
        public bool HasPaid { get; set; } = false;

        // Constructor
        public Student(int id, string name, string lastName, int? dormitoryId = null)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            DormitoryId = dormitoryId;
        }

        // Llogarit shpenzimin mujor (p.sh. dorm + kafeteri)
        public decimal CalculateMonthlyCost(decimal roomFee = 100m, decimal cafeteriaCost = 0m)
        {
            return roomFee + cafeteriaCost;
        }
    }
}

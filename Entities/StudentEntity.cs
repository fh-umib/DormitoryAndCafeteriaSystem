namespace DormitoryAndCafeteriaSystem.Entities
{
    public class StudentEntity
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public int? DormitoryID { get; set; }
    }
}

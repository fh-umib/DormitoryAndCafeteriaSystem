namespace DormitoryAndCafeteriaSystem.Entities
{
    public class DormitoryEntity
    {
        public int DormitoryID { get; set; }
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
        public int TotalCapacity { get; set; }
        public string Status { get; set; } = "";
    }
}

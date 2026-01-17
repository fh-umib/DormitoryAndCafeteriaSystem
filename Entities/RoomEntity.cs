namespace DormitoryAndCafeteriaSystem.Entities
{
    public class RoomEntity
    {
        public int RoomID { get; set; }
        public int DormitoryID { get; set; }
        public string RoomNumber { get; set; } = "";
        public int Capacity { get; set; }
        public string Type { get; set; } = "";
    }
}

//namespace DormitoryAndCafeteriaSystem
//{
//    public class Student
//    {
//        public int Id { get; set; }                   // StudentID
//        public string Name { get; set; } = "";        // FirstName
//        public string LastName { get; set; } = "";    // LastName
//        public string Email { get; set; } = "";
//        public string Phone { get; set; } = "";
//        public int? DormitoryId { get; set; }         // nullable int për DormitoryID

//        public Student(int id, string name, string lastName, int? dormitoryId = null)
//        {
//            Id = id;
//            Name = name;
//            LastName = lastName;
//            DormitoryId = dormitoryId;
//        }

//        // Parameterless constructor për ORM ose serializim
//        public Student() { }

//        public override string ToString()
//        {
//            return $"{Id} | {Name} {LastName} | Dormitory ID: {(DormitoryId.HasValue ? DormitoryId.Value.ToString() : "None")}";
//        }
//    }
//}


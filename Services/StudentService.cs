using DormitoryAndCafeteriaSystem.Repositories;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public void PrintAllStudents()
        {
            var students = _repo.GetAll();
            foreach (var s in students)
            {
                System.Console.WriteLine($"{s.StudentID} | {s.FirstName} {s.LastName}");
            }
        }
    }
}

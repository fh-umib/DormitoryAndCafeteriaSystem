using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Repositories;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public void Save(Student student)
        {
            var entity = new StudentEntity
            {
                StudentID = student.Id,
                FirstName = student.Name,
                LastName = student.LastName,
                DormitoryID = student.DormitoryID.Value
                // përdor DormitoryID ekzistues
            };


            _repo.Insert(entity);
        }
    }
}

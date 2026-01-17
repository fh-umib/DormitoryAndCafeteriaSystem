using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        List<StudentEntity> GetAll();
        StudentEntity? GetById(int id);
        void Add(StudentEntity student);
        void Delete(int id);
    }
}

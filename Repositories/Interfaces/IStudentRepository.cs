using System.Collections.Generic;
using DormitoryAndCafeteriaSystem.Entities;

namespace DormitoryAndCafeteriaSystem.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Remove(int id);
        Student? GetById(int id);
        List<Student> GetAll();
        void Update(Student student);
    }
}

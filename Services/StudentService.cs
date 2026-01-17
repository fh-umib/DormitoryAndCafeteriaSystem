using System;
using System.Collections.Generic;
using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Repositories;
using DormitoryAndCafeteriaSystem.Repositories.Interfaces;

namespace DormitoryAndCafeteriaSystem.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService()
        {
            _repo = new StudentRepository();
        }

        public void RegisterStudent(Student student)
        {
            _repo.Add(student);
            Console.WriteLine("Student registered successfully!");
        }

        public void RemoveStudent(int id)
        {
            _repo.Remove(id);
            Console.WriteLine("Student removed successfully!");
        }

        public Student? GetStudentById(int id) => _repo.GetById(id);

        public List<Student> Students => _repo.GetAll();

        public void ViewAllStudents()
        {
            var students = Students;
            if (students.Count == 0)
            {
                Console.WriteLine(" No students found!");
                return;
            }

            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.Id}, Name: {s.Name} {s.LastName}, Email: {s.Email}, Phone: {s.Phone}, Dorm: {(s.DormitoryId?.ToString() ?? "N/A")}");
            }
        }

        public void ApplyForDorm(Student student, int dormId)
        {
            student.DormitoryId = dormId;
            _repo.Update(student);
            Console.WriteLine($"{student.Name} applied for dorm {dormId}.");
        }

        public void PayMonthlyFee(Student student)
        {
            Console.WriteLine($"Monthly fee paid for {student.Name}.");
        }
    }
}

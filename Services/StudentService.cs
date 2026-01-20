using System;
using System.Collections.Generic;
using System.Linq;
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

        // ================= STUDENT MANAGEMENT =================
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
                Console.WriteLine("No students found!");
                return;
            }

            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.Id}, Name: {s.Name} {s.LastName}, Email: {s.Email}, Phone: {s.Phone}, Dorm: {(s.DormitoryId?.ToString() ?? "N/A")}, Room: {(s.AssignedRoomNumber?.ToString() ?? "N/A")}, Paid: {s.HasPaid}");
            }
        }

        // ================= DORM APPLICATION =================
        public void ApplyForDorm(Student student, int dormitoryId)
        {
            student.AppliedDormitoryId = dormitoryId;
            _repo.Update(student);
            Console.WriteLine($"{student.Name} applied for dormitory {dormitoryId}.");
        }

        public List<Student> GetStudentsByDormitory(int dormId)
        {
            return Students.Where(s => s.AppliedDormitoryId == dormId).ToList();
        }

        // ================= PAYMENT =================
        public void PayMonthlyFee(Student student)
        {
            student.HasPaid = true;
            _repo.Update(student);
            Console.WriteLine($"Monthly fee paid for {student.Name}.");
        }

        // ================= MONTHLY COST =================
        public decimal CalculateMonthlyCost(Student student, decimal roomFee = 100m, decimal cafeteriaCost = 0m)
        {
            return student.CalculateMonthlyCost(roomFee, cafeteriaCost);
        }
    }
}

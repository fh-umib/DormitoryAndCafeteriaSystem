using System;
using System.Linq;
using DormitoryAndCafeteriaSystem.Entities;
using DormitoryAndCafeteriaSystem.Services;

namespace DormitoryAndCafeteriaSystem
{
    class Program
    {
        private static StudentService studentService = new StudentService();
        private static RoomService roomService = new RoomService();
        private static CafeteriaService cafeteriaService = new CafeteriaService();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": RegisterStudent(); break;
                    case "2": studentService.ViewAllStudents(); Pause(); break;
                    case "3": PlaceCafeteriaOrder(); break;
                    case "4": cafeteriaService.ViewAllOrders(); Pause(); break;
                    case "5": ViewStudentsByDormitory(); break;
                    case "6": ViewStudentMonthlyCost(); break;
                    case "7": RemoveStudent(); break;
                    case "8": roomService.ShowDormRules(); Pause(); break;
                    case "9": ApplyForDorm(); break;
                    case "10": AssignRoom(); break;
                    case "11": PayMonthlyFee(); break;
                    case "0": Console.WriteLine("Exiting... Goodbye!"); return;
                    default: Console.WriteLine("Invalid option!"); Pause(); break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("====== DORMITORY & CAFETERIA SYSTEM ======");
            Console.WriteLine("1. Register New Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Place Cafeteria Order");
            Console.WriteLine("4. View All Orders");
            Console.WriteLine("5. View Students By Dormitory");
            Console.WriteLine("6. View Student Monthly Spending");
            Console.WriteLine("7. Remove Student");
            Console.WriteLine("8. View Dorm Rules");
            Console.WriteLine("9. Apply for Dorm");
            Console.WriteLine("10. Assign Room to Student");
            Console.WriteLine("11. Pay Monthly Fee");
            Console.WriteLine("0. Exit");
            Console.WriteLine("==========================================");
            Console.Write("Enter choice: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // ================= STUDENT =================
        static void RegisterStudent()
        {
            try
            {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine()!);

                Console.Write("First Name: ");
                string firstName = Console.ReadLine()!;

                Console.Write("Last Name: ");
                string lastName = Console.ReadLine()!;

                Console.Write("Email: ");
                string email = Console.ReadLine()!;

                Console.Write("Phone: ");
                string phone = Console.ReadLine()!;

                var student = new Student(id, firstName, lastName)
                {
                    Email = email,
                    Phone = phone
                };

                studentService.RegisterStudent(student);
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering student:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        static void RemoveStudent()
        {
            try
            {
                Console.Write("Enter Student ID to remove: ");
                int id = int.Parse(Console.ReadLine()!);
                studentService.RemoveStudent(id);
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error removing student:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        static void ApplyForDorm()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var student = studentService.GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    Pause();
                    return;
                }

                Console.Write("Enter Dormitory ID to apply: ");
                int dormId = int.Parse(Console.ReadLine()!);

                studentService.ApplyForDorm(student, dormId);
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error applying for dorm:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        static void AssignRoom()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var student = studentService.GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    Pause();
                    return;
                }

                roomService.AssignRoomToStudent(student);
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error assigning room:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        static void PayMonthlyFee()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var student = studentService.GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    Pause();
                    return;
                }

                studentService.PayMonthlyFee(student);
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error paying monthly fee:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        static void ViewStudentMonthlyCost()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var student = studentService.GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    Pause();
                    return;
                }

                decimal total = studentService.CalculateMonthlyCost(student, roomService.MonthlyFee(student), cafeteriaService.TotalSpentByStudent(student.Id));
                Console.WriteLine($"Student: {student.Name} {student.LastName}");
                Console.WriteLine($"Total Monthly Spending: {total}€");
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error calculating monthly cost:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        // ================= CAFETERIA =================
        static void PlaceCafeteriaOrder()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int id = int.Parse(Console.ReadLine()!);

                var student = studentService.GetStudentById(id);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    Pause();
                    return;
                }

                cafeteriaService.PlaceOrder(student);
                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error placing order:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }

        // ================= VIEW STUDENTS BY DORM =================
        static void ViewStudentsByDormitory()
        {
            try
            {
                Console.Write("Enter Dormitory ID: ");
                int dormId = int.Parse(Console.ReadLine()!);

                var students = studentService.GetStudentsByDormitory(dormId);
                if (students.Count == 0)
                {
                    Console.WriteLine("No students have applied for this dormitory.");
                    Pause();
                    return;
                }

                foreach (var student in students)
                {
                    Console.WriteLine($"Student: {student.Name} {student.LastName}, Room: {(student.AssignedRoomNumber?.ToString() ?? "N/A")}, Paid: {student.HasPaid}, Applied Dorm ID: {student.AppliedDormitoryId}");
                }

                Pause();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error viewing students by dormitory:");
                Console.WriteLine(ex.Message);
                Pause();
            }
        }
    }
}

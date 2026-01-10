using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace DormitoryAndCafeteriaSystem
{
    class Program
    {
        
        
        static List<Student> students = new();
        static CafeteriaSystem cafeteria = new();
        static DormitoryRules rules = new();
        static DormitoryApplication application = new();
        static AccomodationAssignment assignment = new();
        static Payment payment = new();
        static Room sampleRoom = new Room(101, 2);
        static List<Room> rooms = new List<Room>();


        static void Main()
        {
            // Set console encoding to UTF-8 to support special characters 
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            InitializeRooms();
            LoadAllStudents();

            while (true)
            {
                Console.Clear();
                DisplayMenu();
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": // Register New Student
                        RegisterStudent();
                        break;

                    case "2": // View All Students
                        ViewAllStudents();
                        break;

                    case "3": // Place Cafeteria Order
                        cafeteria.PlaceOrder(students);
                        break;

                    case "4": // View All Orders
                        cafeteria.ViewAllOrders();
                        break;

                    case "5": // View Orders By Dormitory
                        ViewOrdersByDormitory();
                        break;

                    case "6": // View Student Monthly Cost
                        ViewStudentMonthlyCost();
                        break;

                    case "7": // Remove Student
                        RemoveStudent();
                        break;
                    case "8":
                        rules.ShowRules();
                        Pause();
                        break;

                    case "9":
                        {
                            var student = SelectStudent();
                            if (student == null) Console.WriteLine("Student not found!");
                            else application.Apply(student);
                            Pause();
                            break;
                        }

                    case "10":
                        {
                            var student = SelectStudent();
                            if (student == null) Console.WriteLine("Student not found!");
                            else assignment.AssignRoom(student, rooms);
                            Pause();
                            break;
                        }

                    case "11":
                        {
                            var student = SelectStudent();
                            if (student == null) Console.WriteLine("Student not found!");
                            else payment.PayMonthlyFee(student);
                            Pause();
                            break;
                        }
                    case "12":
                        {
                            foreach (var student in students)
                            {
                                student.ResetMonthlyData();
                                student.SaveToFile($"student_{student.Id}.json");
                            }

                            Console.WriteLine("✅ NEW MONTH STARTED");
                            Console.WriteLine("➡ Cafeteria debt reset to 0€");
                            Console.WriteLine("➡ Monthly payments reset");
                            Pause();
                            break;
                        }

                    case "13": // Exit
                        SaveAllData();
                        Console.WriteLine("Exiting... Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        Pause();
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("====== DORMITORY & CAFETERIA SYSTEM ======");
            Console.WriteLine("1. Register New Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Place Cafeteria Order");
            Console.WriteLine("4. View All Orders");
            Console.WriteLine("5. View Orders By Dormitory");
            Console.WriteLine("6. View Student Monthly Cost");
            Console.WriteLine("7. Remove Student");
            Console.WriteLine("8. View dorm rules");
            Console.WriteLine("9. Apply for dorm");
            Console.WriteLine("10. Assign room to student");
            Console.WriteLine("11. Pay monthly fee");
            Console.WriteLine("12. Reset monthly data (admin)");
            Console.WriteLine("13. Exit");

            Console.WriteLine("==========================================\n");
            Console.Write("Enter choice: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // -------------------- CASE METHODS --------------------

        static void RegisterStudent()
        {
            int id;
            while (true)
            {
                Console.Write("ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id)) break;
                Console.WriteLine("Invalid input!");
            }

            Console.Write("Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Last Name: ");
            string lastname = Console.ReadLine() ?? string.Empty;

            Console.Write("Dormitory: ");
            string dormitory = Console.ReadLine() ?? string.Empty;

            var student = new Student(id, name,lastname, dormitory);
            students.Add(student);
            student.SaveToFile($"student_{id}.json");

            Console.WriteLine("Student registered successfully.");
            Pause();
        }

        static void ViewAllStudents()
        {
            if (students.Count == 0)
                Console.WriteLine("No students found.");
            else
            {
                Console.WriteLine("\nAll Students:");
                foreach (var student in students)
                {
                    Console.WriteLine(student); // Student ToString() tregon ID, Name, Dormitory
                }
            }
            Pause();
        }

        static void ViewOrdersByDormitory()
        {
            Console.Write("Dormitory: ");
            string dorm = Console.ReadLine() ?? "";
            cafeteria.ViewOrdersByDormitory(students, dorm);
            Pause();
        }

        static void ViewStudentMonthlyCost()
        {
            int id;
            while (true)
            {
                Console.Write("Student ID: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id)) break;
                Console.WriteLine("Invalid input!");
            }

            var student = students.Find(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                Pause();
                return;
            }

            Console.WriteLine($"{student.Name} | Dormitory: {student.Dormitory} | Monthly Fee: {student.CalculateMonthlyCost()}€");
            Console.WriteLine($"Total Spent in Cafeteria: {cafeteria.TotalSpentByStudent(id)}€");
            Pause();
        }

        static void RemoveStudent()
        {
            int id;
            while (true)
            {
                Console.Write("Student ID to remove: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out id)) break;
                Console.WriteLine("Invalid input!");
            }

            students.RemoveAll(s => s.Id == id);
            cafeteria.RemoveOrdersByStudent(id);

            if (File.Exists($"student_{id}.json")) File.Delete($"student_{id}.json");

            Console.WriteLine("Student and related orders removed.");
            Pause();
        }

        static void LoadAllStudents()
        {
            foreach (var file in Directory.GetFiles(".", "student_*.json"))
            {
                Student? s = Student.LoadFromFile(file);
                if (s != null) students.Add(s);
            }
        }

        static void SaveAllData()
        {
            foreach (var s in students) s.SaveToFile($"student_{s.Id}.json");
            cafeteria.SaveAllOrders();
            foreach (var room in rooms)
            {
                room.Save($"room_{room.RoomNumber}.json");
            }

        }

        static void InitializeRooms()
        {
            rooms.Clear();

            // 1️⃣ Provo me i ngarku dhomat nga JSON
            foreach (var file in Directory.GetFiles(".", "room_*.json"))
            {
                try
                {
                    rooms.Add(Room.Load(file));
                }
                catch
                {
                    Console.WriteLine($"Failed to load {file}");
                }
            }

            // 2️⃣ Nëse nuk ka asnjë room.json, krijo dhoma të reja
            if (rooms.Count == 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    rooms.Add(new Room(i, 2));
                }
            }
        }

        static Student? SelectStudent()
        {
            Console.Write("Enter student ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return null;

            return students.FirstOrDefault(s => s.Id == id);
        }




    }
}

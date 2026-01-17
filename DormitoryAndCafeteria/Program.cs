using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Npgsql;


namespace DormitoryAndCafeteriaSystem
{
    // Klasa kryesore e aplikacionit (Entry Point).
    // Koordinon rrjedhen e programit dhe therret sherbimet perkatese,
    // pa permbajtur logjike biznesi.
    class Program
    {
        
        static List<Student> students = new();
        static CafeteriaSystem cafeteria = new();
        static DormitoryRules rules = new();
        static DormitoryApplication application = new();
        static AccomodationAssignment assignment = new();
        static Payment payment = new();
        static List<Room> rooms = new List<Room>();
        
        

        static void Main()
        {
            
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;



            // Testo lidhjen me databazën para se të hapet menuja
            DormitoryAndCafeteriaSystem.Data.DatabaseTester.TestDatabase();

            InitializeRooms();

            

            //LoadAllStudents();

            DisplayMenu();

            // Set console encoding to UTF-8 to support special characters 
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            InitializeRooms();
            //LoadAllStudents();

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

                    case "6": // View Student Monthly Spending
                        ViewStudentMonthlyCost();
                        break;

                    case "7": // Remove Student
                        RemoveStudent();
                        break;
                    case "8": // Show Rules
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

                            Console.WriteLine("NEW MONTH STARTED");
                            Console.WriteLine("Cafeteria debt reset to 0€");
                            Console.WriteLine("Monthly payments reset");
                            Pause();
                            break;
                        }
                    case "13":
                        {
                            var student = SelectStudent();
                            if (student == null)
                                Console.WriteLine("Student not found.");
                            else
                                assignment.Checkout(student, rooms);

                            Pause();
                            break;
                        }
                    case "0": // Exit
                        //SaveAllData();
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
            Console.WriteLine("6. View Student Monthly Spending");
            Console.WriteLine("7. Remove Student");
            Console.WriteLine("8. View dorm rules");
            Console.WriteLine("9. Apply for dorm");
            Console.WriteLine("10. Assign room to student");
            Console.WriteLine("11. Pay monthly fee");
            Console.WriteLine("12. Reset monthly data");
            Console.WriteLine("13. Checkout student (show assigned room)");
            Console.WriteLine("0. Exit");

            Console.WriteLine("==========================================\n");
            Console.Write("Enter choice: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // -------------------- Metodat e case-ave --------------------

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

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Phone: ");
            string phone = Console.ReadLine() ?? string.Empty;

            Console.Write("Dormitory ID (optional): ");
            string dormInput = Console.ReadLine();
            int? dormId = null;
            if (!string.IsNullOrWhiteSpace(dormInput) && int.TryParse(dormInput, out int did))
                dormId = did;

            try
            {
                using var conn = new NpgsqlConnection("Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;");
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "INSERT INTO Student (StudentID, FirstName, LastName, Email, Phone, DormitoryID) " +
                    "VALUES (@id, @fname, @lname, @email, @phone, @dorm);", conn);

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("fname", name);
                cmd.Parameters.AddWithValue("lname", lastname);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("phone", phone);
                if (dormId.HasValue) cmd.Parameters.AddWithValue("dorm", dormId.Value);
                else cmd.Parameters.AddWithValue("dorm", DBNull.Value);

                cmd.ExecuteNonQuery();
                // Shto studentin edhe në listën në memorje
                var student = new Student(id, name, lastname, dormId.HasValue ? dormId.Value.ToString() : "");
                students.Add(student);


                Console.WriteLine("Student registered successfully in database!");

                
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gabim gjatë regjistrimit: {ex.Message}");
            }

            Pause();
        }

        

        static void ViewAllStudents()
        {
            try
            {
                using var conn = new NpgsqlConnection("Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;");
                conn.Open();

                using var cmd = new NpgsqlCommand("SELECT s.StudentID, s.FirstName, s.LastName, d.Name AS DormName " +
                                                  "FROM Student s LEFT JOIN Dormitory d ON s.DormitoryID = d.DormitoryID " +
                                                  "ORDER BY s.StudentID;", conn);

                using var reader = cmd.ExecuteReader();
                Console.WriteLine("\nAll Students:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["StudentID"]} | {reader["FirstName"]} {reader["LastName"]} | Dormitory: {reader["DormName"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gabim gjatë leximit të studentëve: {ex.Message}");
            }

            Pause();
        }

        static void ViewOrdersByDormitory() //case 5
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

        //static void LoadAllStudents()
        //{
        //    foreach (var file in Directory.GetFiles(".", "student_*.json"))
        //    {
        //        Student? s = Student.LoadFromFile(file);
        //        if (s != null) students.Add(s);
        //    }
        //}

        //static void SaveAllData()
        //{
        //    foreach (var s in students) s.SaveToFile($"student_{s.Id}.json");
        //    cafeteria.SaveAllOrders();
        //    foreach (var room in rooms)
        //    {
        //        room.Save($"room_{room.RoomNumber}.json");
        //    }

        //}

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

            // 2️⃣ Nese nuk ka asnje room.json, krijo dhoma te reja
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
        static void LoadAllStudentsFromDb()
        {
            students.Clear();
            using var conn = new Npgsql.NpgsqlConnection(
                "Host=localhost;Port=5432;Database=DormitoryAndCafeteriaSystemDB;Username=postgres;Password=2206;");
            conn.Open();

            using var cmd = new Npgsql.NpgsqlCommand(
                "SELECT StudentID, FirstName, LastName, DormitoryID FROM Student ORDER BY StudentID;", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string lastName = reader.GetString(2);
                string dormitory = reader.IsDBNull(3) ? "" : reader.GetString(3);

                var student = new Student(id, name, lastName, dormitory);
                students.Add(student);
            }
        }



    }
}

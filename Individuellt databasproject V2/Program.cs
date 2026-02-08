using Microsoft.Data.SqlClient;

namespace Individuellt_databasproject_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool whileRunning = true;

            while (whileRunning)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till skolhanteringssystemet!\n" +
                    "Välj ett alternativ:\n" +
                    "1. Visa alla studenter\n" +
                    "2. Visa lärare på de olika avdelningarna\n" +
                    "3. Visa klass information \n" +
                    "4. Visa anställda\n" +
                    "5. Visa betyg på elev\n" +
                    "6. Se lönen på de olika avdelningarna\n" +
                    "7. Visa information om en specifik elev\n" +
                    "8. Sätt betyg på en elev\n" +
                    "9. Lägg till ny personal\n" +
                    "0. Avsluta");

                // Read user input
                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        EFMenuActions.ShowAllStudents();
                        Console.ReadKey();
                        break;

                    case "2":
                        EFMenuActions.ShowTeachersPerDepartment();
                        Console.ReadKey();
                        break;

                    case "3":
                        EFMenuActions.ShowAllClasses();
                        Console.ReadKey();
                        break;

                    case "4":
                        AdoNetFunctions.ShowStaff();
                        Console.ReadKey();
                        break;

                    case "5":
                        AdoNetFunctions.ShowStudentsSimple();

                        Console.Write("Ange student ID för att visa betyg: ");
                        string input = Console.ReadLine();
                        Console.Clear();

                        if (int.TryParse(input, out int studentId))
                        {
                            AdoNetFunctions.ShowGradeForStudents(studentId);
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt ID, försök igen.");
                        }
                        Console.ReadKey();
                        break;

                    case "6":
                        AdoNetFunctions.ShowDepartmentSalaries();
                        Console.ReadKey();
                        break;

                    case "7":
                        AdoNetFunctions.ShowStudentsSimple();

                        Console.Write("Ange student ID för visa information: ");
                        string studentInfoInput = Console.ReadLine();

                        Console.Clear();

                        // Validate input and call the method if it's a valid integer
                        if (int.TryParse(studentInfoInput, out int studentID))
                        {
                            AdoNetFunctions.ShowStudentById(studentID);
                        }
                        // If the input is not a valid integer, display an error message
                        else
                        {
                            Console.WriteLine("Ogiltigt ID, försök igen.");
                        }
                        Console.ReadKey();
                        break;

                    case "8":
                        HandleAddGrade();
                        Console.ReadKey();
                        break;

                    case "9":
                        AdoNetFunctions.HandleAddStaff();
                        Console.ReadKey();
                        break;

                    case "0":
                        Console.WriteLine("Stänger av program, klicka valfri knapp en gång till");
                        Console.ReadKey();
                        whileRunning = false;
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }
            }
        }

        //Method to handle user input in addgrade method
        private static void HandleAddGrade()
        {
            Console.Clear();

            //Show and pick student
            AdoNetFunctions.ShowStudentsSimple();
            Console.Write("\nVälj student ID: ");

            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Ogiltigt student-ID.");
                return;
            }
            Console.Clear();

            //Show and pick subject
            AdoNetFunctions.ShowSubjectsSimple();
            Console.Write("\nVälj ämnes-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int subjectId))
            {
                Console.WriteLine("Ogiltigt ämnes-ID.");
                return;
            }
            Console.Clear();

            //Show and pick teacher
            AdoNetFunctions.ShowTeachersSimple();
            Console.Write("\nVälj lärar-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int staffId))
            {
                Console.WriteLine("Ogiltigt lärar-ID.");
                return;
            }
            Console.Clear();

            Console.Write("Ange betyg (t.ex A–F): ");
            string grade = Console.ReadLine();

            AdoNetFunctions.AddGrade(studentId, subjectId, staffId, grade);
            Console.ReadKey();
        }

        //// Method to handle user input in addstaff method
        //private static void HandleAddStaff()
        //{
        //    Console.Clear();
        //    Console.WriteLine("\tLägg till ny personal");

        //    Console.Write("Förnamn: ");
        //    string firstName = Console.ReadLine();

        //    Console.Write("Efternamn: ");
        //    string lastName = Console.ReadLine();

        //    Console.Write("Personnummer: ");
        //    string ssn = Console.ReadLine();

        //    // Show and pick position
        //    int positionId;
        //    using (SqlConnection connection = new SqlConnection(AdoNetFunctions.connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT ID, PositionName FROM Positions", connection);
        //        connection.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        Console.WriteLine("Tillgängliga Positioner:");
        //        while (reader.Read())
        //            Console.WriteLine($"{reader["ID"]}: {reader["PositionName"]}");
        //    }

        //    // Validate position input
        //    while (!int.TryParse(Console.ReadLine(), out positionId))
        //        Console.Write("Ogiltigt Position ID, försök igen: ");

        //    // Show and pick department
        //    int departmentId;
        //    using (SqlConnection connection = new SqlConnection(AdoNetFunctions.connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT ID, DepartmentName FROM Departments", connection);
        //        connection.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        Console.WriteLine("\nTillgängliga Avdelningar:");
        //        while (reader.Read())
        //            Console.WriteLine($"{reader["ID"]}: {reader["DepartmentName"]}");
        //    }

        //    // Validate department input
        //    while (!int.TryParse(Console.ReadLine(), out departmentId))
        //        Console.Write("Ogiltigt Avdelnings ID, försök igen: ");

        //    // Salary input and validation
        //    decimal salary;
        //    while (!decimal.TryParse(Console.ReadLine(), out salary))
        //        Console.Write("Ogiltig lön, försök igen: ");

        //    // Hire date input and validation
        //    DateTime hireDate;
        //    while (!DateTime.TryParse(Console.ReadLine(), out hireDate))
        //        Console.Write("Ogiltigt datumformat, försök igen (YYYY-MM-DD): ");

        //    // Add staff to the database
        //    AdoNetFunctions.AddStaff(firstName, lastName, ssn, positionId, departmentId, salary, hireDate);
        //    Console.WriteLine("\n Ny personal har lagts till!");
        //    Console.ReadKey();
        //}
    }
}

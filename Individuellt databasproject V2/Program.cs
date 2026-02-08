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
                        HandleAddStaff();
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

        // Method to handle user input in addstaff method
        private static void HandleAddStaff()
        {
            Console.Clear();
            Console.WriteLine("\tLägg till ny personal");

            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Personnumer: ");
            string ssn = Console.ReadLine();

            // Show positions and get position ID with validation
            Console.Write("Positions ID:");
            if(!int.TryParse(Console.ReadLine(), out int positionId))
            {
                Console.WriteLine("Ogiltigt Positions ID");
                return;
            }

            // Show departments and get department ID with validation
            Console.Write("Avdelnings ID:");
            if(!int.TryParse(Console.ReadLine(), out int departmentId))
            {
                Console.WriteLine("Ogiltigt Avdelnings ID");
                return;
            }

            // Salary input with validation
            Console.Write("Lön:");
            if(!decimal.TryParse(Console.ReadLine(), out decimal salary))
            {
                Console.WriteLine("Ogiltig lön");
                return;
            }

            // Hire date input with validation
            Console.Write("Anställningsdatum (YYYY-MM-DD): ");
            if(!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
            {
                Console.WriteLine("Ogiltigt datumformat");
                return;
            }

            // Call the method to add staff with the collected and validated input
            AdoNetFunctions.AddStaff(firstName, lastName, ssn, positionId, departmentId, salary, hireDate);
            Console.WriteLine("Personal sparad!");
        }
    }
}

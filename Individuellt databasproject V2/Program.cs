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
}

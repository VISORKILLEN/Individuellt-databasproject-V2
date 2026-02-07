using Individuellt_databasproject_V2.Models;
using Microsoft.EntityFrameworkCore;


namespace Individuellt_databasproject_V2
{
    internal class EFMenuActions
    {
        // Show all students with their class names
        public static void ShowAllStudents()
        {
            using var db = new FinalSchoolAppContext();


            // Link query to include class information
            var students = db.Students
                .Include(s => s.Class)
                .ToList();

            // Print results
            foreach (var s in students)
            {
                Console.WriteLine($"Student ID: {s.Id}, Namn: {s.FirstName} {s.LastName}, Klass: {s.Class?.ClassName}");
            }
        }


        // Show number of teachers per department
        public static void ShowTeachersPerDepartment()
        {

            // Link query to group teachers by department and count them
            using var db = new FinalSchoolAppContext();
            var departments = db.Staff
                    .Where(s => s.Position.PositionName == "Lärare")
                    .GroupBy(s => s.Department.DepartmentName)
                    .Select(g => new
                    {
                        Department = g.Key,
                        StaffCount = g.Count(),
                    })
                    .ToList();

            // Print results
            foreach (var dept in departments)
            {
                Console.WriteLine($"Avdelning: {dept.Department}, Antal Lärare: {dept.StaffCount}");
            }
        }

        // Show all classes with their mentors
        public static void ShowAllClasses()
        {
            using var db = new FinalSchoolAppContext();

            var classes = db.Classes.Include(c => c.Mentor).ToList();

            // Print results
            foreach (var c in classes)
            {
                Console.WriteLine($"Klass: {c.ClassName}, Mentor: {c.Mentor.FirstName} {c.Mentor.Lastname}");
            }
        }

    }
}

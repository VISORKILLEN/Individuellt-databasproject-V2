using Microsoft.Data.SqlClient;
using System.Data;

namespace Individuellt_databasproject_V2
{
    internal class AdoNetFunctions
    {

        private static string connectionString = "Server=localhost;Database=Final_School_App;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void ShowStaff(string position = null)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            // Create command to execute stored procedure
            SqlCommand cmd = new SqlCommand("GetStaff", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add position parameter if provided
            if (!string.IsNullOrEmpty(position))
            {
                cmd.Parameters.AddWithValue("@Position", position);
            }

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();


            // Print staff details
            Console.WriteLine("All personal:");
            while (reader.Read())
            {
                Console.WriteLine($"Namn: {reader["FirstName"]} {reader["LastName"]}," +
                    $"Position: {reader["Position"]}, " +
                    $"Avdelning: {reader["Department"]}, " +
                    $"Lön: {reader["Salary"]} KR, " +
                    $"År som anställd på skolan: {reader["YearsAtSchool"]}\n");
            }
        }


        // Show grades for a specific student, including subject and teacher information
        public static void ShowGradeForStudents(int studentId)
        {
            using SqlConnection connection = new SqlConnection(connectionString);


            // SQL query to get grades for a specific student, including subject and teacher information
            string sql = @"
                    SELECT 
                        stu.FirstName AS StudentFirstName,
                        stu.LastName  AS StudentLastName,
                        sub.SubjectName,
                        g.Grade,
                        g.Dates,
                        st.FirstName AS TeacherFirstName,
                        st.LastName  AS TeacherLastName
                    FROM Grade g
                    JOIN Student stu ON g.StudentID = stu.ID
                    JOIN Subjects sub ON g.SubjectID = sub.ID
                    JOIN Staff st     ON g.StaffID = st.ID
                    WHERE g.StudentID = @StudentId
                    ORDER BY g.Dates;";

            SqlCommand cmd = new SqlCommand(sql, connection);

            // Add parameter to prevent SQL injection
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            // Open connection and execute query
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Print grade details
            bool gradesFound = false;
            string studentName = "";

            while (reader.Read())
            {
                // Print student name only once
                if (!gradesFound)
                {
                    studentName = $"{reader["StudentFirstName"]} {reader["StudentLastName"]}";
                    Console.WriteLine($"Betyg för student: {studentName}\n");
                    gradesFound = true;
                }

                // Print each grade entry
                Console.WriteLine(
                    $"Ämne: {reader["SubjectName"]}, " +
                    $"Betyg: {reader["Grade"]}, " +
                    $"Lärare: {reader["TeacherFirstName"]} {reader["TeacherLastName"]}, " +
                    $"Datum: {reader["Dates"]}"
                );
            }

            // If no grades were found, inform the user
            if (!gradesFound)
            {
                Console.WriteLine("Inga betyg hittades för den angivna studenten.");
            }
        }

        // Show total and average salary per department
        public static void ShowDepartmentSalaries()
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            // SQL query to calculate total and average salary per department
            string sql = @"
                    SELECT 
                        d.DepartmentName,
                        SUM(s.Salary) AS TotalSalary,
                        AVG(s.Salary) AS AverageSalary
                    FROM Staff s
                    JOIN Departments d ON s.DepartmentID = d.ID
                    GROUP BY d.DepartmentName;";

            // Create command and execute query
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Löner per avdelning:");

            // Print salary details for each department
            while (reader.Read())
            {

                // Format total and average salary to two decimal places and include "KR" for currency
                Console.WriteLine(
                    $"Avdelning: {reader["DepartmentName"]}, " +
                    $"Total lön på avdelningen: {((decimal)reader["TotalSalary"]):N2} KR, " +
                    $"Genomsnittlig lön på avdelning: {((decimal)reader["AverageSalary"]):N2} KR\n"
                );
            }
        }

        // Show student information by ID using a stored procedure
        public static void ShowStudentById(int studentID)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            // Create command to execute stored procedure
            SqlCommand cmd = new SqlCommand("GetStudentInfoById", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentId", studentID);

            // Open connection and execute query
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Print student details if found
            if (reader.Read())
            {
                
                Console.WriteLine($"\n\tElevinformation\n" +
                    $"Namn: {reader["FirstName"]} {reader["LastName"]}\n" +
                    $"Personnummer: {reader["SocialSecurityNumber"]}\n" +
                    $"Klass: {reader["ClassName"]}");
            }

            // If no student was found, inform the user
            else
            {
                Console.WriteLine("Ingen student hittades med det ID:t");
            }
        }


        public static void AddGrade(int studentId, int subjectId, int staffId, string grade)
        {

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();


            SqlTransaction transaction = connection.BeginTransaction();

            // Create command to execute stored procedure
            try
            {

                
                SqlCommand cmd = new SqlCommand("AddGrade", connection, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters for the stored procedure
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                cmd.Parameters.AddWithValue("@Grade", grade);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                // Execute the command to insert the grade
                cmd.ExecuteNonQuery();
                transaction.Commit();

                Console.WriteLine("Betyg har sparats!");
            }

            // If an error occurs, roll back the transaction and display an error message
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            }
        }

        // Displays a list of all students with their ID and full name
        public static void ShowStudentsSimple()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT ID, FirstName, LastName FROM Student ORDER BY LastName";

            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Studenter:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ID"]}: {reader["FirstName"]} {reader["LastName"]}");
            }
        }

        // Displays a list of all teachers
        public static void ShowTeachersSimple()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string sql = @"
                        SELECT s.ID, s.FirstName, s.LastName
                        FROM Staff s
                        JOIN Positions p ON s.PositionID = p.ID
                        WHERE p.PositionName = 'Lärare'
                        ORDER BY s.LastName";

            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Lärare:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ID"]}: {reader["FirstName"]} {reader["LastName"]}");
            }
        }

        // Displays a list of all subjects with their ID and name
        public static void ShowSubjectsSimple()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT ID, SubjectName FROM Subjects";

            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Ämnen:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ID"]}: {reader["SubjectName"]}");
            }
        }

        public static void AddStaff(
            string firstName,
            string lastName,
            string ssn,
            int positionId,
            int departmentId,
            decimal salary,
            DateTime hireDate)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("AddStaff", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@SocialSecurityNumber", ssn);
            cmd.Parameters.AddWithValue("@PositionID", positionId);
            cmd.Parameters.AddWithValue("@DepartmentID", departmentId);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@HireDate", hireDate);

            connection.Open();
            cmd.ExecuteNonQuery();

            Console.WriteLine("Ny personal har lagt till! :)");
        }
    }
}



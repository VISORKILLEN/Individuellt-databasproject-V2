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
    }
}



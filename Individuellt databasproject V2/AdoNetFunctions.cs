using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT sub.SubjectName, g.Grade, g.Dates, s.FirstName, s.LastName
                    FROM Grade g
                    JOIN Subjects sub ON g.SubjectsID = s.ID
                    WHERE g.StudentID = @StudentId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();



            }
        }

    }
}

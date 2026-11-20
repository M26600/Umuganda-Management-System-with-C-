
using System;
using MySql.Data.MySqlClient;

namespace UmugandaManagementSystem
{
    public class DatabaseTest
    {
        public static void TestConnection()
        {
            DatabaseConnection db = new DatabaseConnection();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();
                Console.WriteLine("Database connected successfully!");
            }
        }
    }
}

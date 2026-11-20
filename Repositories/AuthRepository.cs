using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UmugandaManagementSystem.Models;

namespace UmugandaManagementSystem.Repositories
{
    public class AuthRepository
    {
        private DatabaseConnection db = new DatabaseConnection();

        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }

        public bool RegisterCoordinator(
            string firstName,
            string lastName,
            string phone,
            int cellId,
            string username,
            string password)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string coordinatorQuery =
                            @"INSERT INTO coordinators
                            (first_name, last_name, phone, cell_id)
                            VALUES (@firstName, @lastName, @phone, @cellId)";

                        using (MySqlCommand cmd =
                            new MySqlCommand(coordinatorQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@firstName", firstName);
                            cmd.Parameters.AddWithValue("@lastName", lastName);
                            cmd.Parameters.AddWithValue("@phone", phone);
                            cmd.Parameters.AddWithValue("@cellId", cellId);

                            cmd.ExecuteNonQuery();

                            int coordinatorId = (int)cmd.LastInsertedId;

                            string userQuery =
                                @"INSERT INTO users
                                (coordinator_id, username, password_hash)
                                VALUES (@coordinatorId, @username, @passwordHash)";

                            using (MySqlCommand userCmd =
                                new MySqlCommand(userQuery, conn, transaction))
                            {
                                userCmd.Parameters.AddWithValue(
                                    "@coordinatorId", coordinatorId);

                                userCmd.Parameters.AddWithValue(
                                    "@username", username);

                                userCmd.Parameters.AddWithValue(
                                    "@passwordHash", HashPassword(password));

                                userCmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Coordinator Login(string username, string password)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query =
                    @"SELECT c.coordinator_id, c.first_name,
                             c.last_name, c.phone, c.cell_id
                      FROM users u
                      INNER JOIN coordinators c
                      ON u.coordinator_id = c.coordinator_id
                      WHERE u.username = @username
                      AND u.password_hash = @passwordHash";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue(
                        "@passwordHash", HashPassword(password));

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Coordinator
                            {
                                CoordinatorId = Convert.ToInt32(
                                    reader["coordinator_id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                Phone = reader["phone"].ToString(),
                                CellId = Convert.ToInt32(reader["cell_id"])
                            };
                        }
                    }
                }
            }

            return null;
        }
        public bool UpdateCoordinatorProfile(
    int coordinatorId,
    string firstName,
    string lastName,
    string phone)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
            UPDATE coordinators
            SET first_name = @firstName,
                last_name = @lastName,
                phone = @phone
            WHERE coordinator_id = @coordinatorId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@coordinatorId", coordinatorId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ChangePassword(
            int coordinatorId,
            string currentPassword,
            string newPassword)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
            UPDATE users
            SET password_hash = @newPasswordHash
            WHERE coordinator_id = @coordinatorId
            AND password_hash = @currentPasswordHash";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@newPasswordHash", HashPassword(newPassword));

                    cmd.Parameters.AddWithValue(
                        "@currentPasswordHash", HashPassword(currentPassword));

                    cmd.Parameters.AddWithValue(
                        "@coordinatorId", coordinatorId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

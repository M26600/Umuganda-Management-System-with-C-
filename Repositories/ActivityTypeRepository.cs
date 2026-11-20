
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UmugandaManagementSystem.Models;

namespace UmugandaManagementSystem.Repositories
{
    public class ActivityTypeRepository
    {
        private DatabaseConnection db = new DatabaseConnection();

        public List<ActivityType> GetAllActivityTypes()
        {
            List<ActivityType> activityTypes =
                new List<ActivityType>();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT activity_type_id, type_name
                    FROM activity_types
                    ORDER BY type_name";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ActivityType activityType = new ActivityType
                        {
                            ActivityTypeId =
                                Convert.ToInt32(reader["activity_type_id"]),

                            TypeName =
                                reader["type_name"].ToString()
                        };

                        activityTypes.Add(activityType);
                    }
                }
            }

            return activityTypes;
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmugandaManagementSystem.Models;

namespace UmugandaManagementSystem.Repositories
{
    public class VillageRepository
    {
        private DatabaseConnection db = new DatabaseConnection();

        // 1. Get all villages belonging to a specific cell
        public List<Village> GetVillagesByCell(int cellId)
        {
            List<Village> villages = new List<Village>();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT village_id, village_name, village_code,
                           leader_name, cell_id
                    FROM villages
                    WHERE cell_id = @cellId
                    ORDER BY village_name";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Village village = new Village
                            {
                                VillageId = Convert.ToInt32(reader["village_id"]),
                                VillageName = reader["village_name"].ToString(),
                                VillageCode = reader["village_code"].ToString(),
                                LeaderName = reader["leader_name"].ToString(),
                                CellId = Convert.ToInt32(reader["cell_id"])
                            };

                            villages.Add(village);
                        }
                    }
                }
            }

            return villages;
        }

        // 2. Add a village
        public bool AddVillage(
            string villageName,
            string villageCode,
            string leaderName,
            int cellId)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO villages
                    (village_name, village_code, leader_name, cell_id)
                    VALUES
                    (@villageName, @villageCode, @leaderName, @cellId)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@villageName", villageName);
                    cmd.Parameters.AddWithValue("@villageCode", villageCode);
                    cmd.Parameters.AddWithValue("@leaderName", leaderName);
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 3. Update a village belonging to the coordinator's cell
        public bool UpdateVillage(
            int villageId,
            string villageName,
            string villageCode,
            string leaderName,
            int cellId)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE villages
                    SET village_name = @villageName,
                        village_code = @villageCode,
                        leader_name = @leaderName
                    WHERE village_id = @villageId
                    AND cell_id = @cellId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@villageName", villageName);
                    cmd.Parameters.AddWithValue("@villageCode", villageCode);
                    cmd.Parameters.AddWithValue("@leaderName", leaderName);
                    cmd.Parameters.AddWithValue("@villageId", villageId);
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 4. Delete a village belonging to the coordinator's cell
        public bool DeleteVillage(int villageId, int cellId)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    DELETE FROM villages
                    WHERE village_id = @villageId
                    AND cell_id = @cellId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@villageId", villageId);
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

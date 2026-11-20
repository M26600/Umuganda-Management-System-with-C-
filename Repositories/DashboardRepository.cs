using System;
using MySql.Data.MySqlClient;
using UmugandaManagementSystem.Models;

namespace UmugandaManagementSystem.Repositories
{
    public class DashboardRepository
    {
        private DatabaseConnection db =
            new DatabaseConnection();

        public DashboardSummary GetDashboardSummary(int cellId)
        {
            DashboardSummary summary = new DashboardSummary();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT

                    (
                        SELECT COUNT(*)
                        FROM villages
                        WHERE cell_id = @cellId
                    ) AS totalVillages,

                    (
                        SELECT COUNT(*)
                        FROM activities a
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                    ) AS totalActivities,

                    (
                        SELECT COUNT(*)
                        FROM activities a
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                        AND a.status = 'PLANNED'
                    ) AS plannedActivities,

                    (
                        SELECT COUNT(*)
                        FROM activities a
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                        AND a.status = 'ONGOING'
                    ) AS ongoingActivities,

                    (
                        SELECT COUNT(*)
                        FROM activities a
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                        AND a.status = 'ACCOMPLISHED'
                    ) AS accomplishedActivities,

                    (
                        SELECT COUNT(*)
                        FROM activities a
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                        AND a.status = 'POSTPONED'
                    ) AS postponedActivities,

                    (
                        SELECT COUNT(*)
                        FROM activities a
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                        AND a.status = 'PLANNED'
                        AND a.activity_date >= CURDATE()
                    ) AS upcomingActivities,

                    (
                        SELECT COUNT(*)
                        FROM activity_reports ar
                        INNER JOIN activities a
                            ON ar.activity_id = a.activity_id
                        INNER JOIN villages v
                            ON a.village_id = v.village_id
                        WHERE v.cell_id = @cellId
                    ) AS submittedReports";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.TotalVillages =
                                Convert.ToInt32(reader["totalVillages"]);

                            summary.TotalActivities =
                                Convert.ToInt32(reader["totalActivities"]);

                            summary.PlannedActivities =
                                Convert.ToInt32(reader["plannedActivities"]);

                            summary.OngoingActivities =
                                Convert.ToInt32(reader["ongoingActivities"]);

                            summary.AccomplishedActivities =
                                Convert.ToInt32(reader["accomplishedActivities"]);

                            summary.PostponedActivities =
                                Convert.ToInt32(reader["postponedActivities"]);

                            summary.UpcomingPlannedActivities =
                                Convert.ToInt32(reader["upcomingActivities"]);

                            summary.SubmittedReports =
                                Convert.ToInt32(reader["submittedReports"]);
                        }
                    }
                }
            }

            return summary;
        }
    }
}
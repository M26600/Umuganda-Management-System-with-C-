
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UmugandaManagementSystem.Models;
using System.Data;
namespace UmugandaManagementSystem.Repositories
{
    public class ActivityReportRepository
    {
        private DatabaseConnection db = new DatabaseConnection();

        // Get accomplished activities that do not have reports yet
        public List<Activity> GetEligibleActivitiesByCell(int cellId)
        {
            List<Activity> activities = new List<Activity>();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT
                        a.activity_id,
                        a.activity_date,
                        a.objective,
                        a.status,
                        a.village_id,
                        a.activity_type_id,
                        a.coordinator_id,
                        v.village_name,
                        at.type_name
                    FROM activities a
                    INNER JOIN villages v
                        ON a.village_id = v.village_id
                    INNER JOIN activity_types at
                        ON a.activity_type_id = at.activity_type_id
                    WHERE a.status = 'ACCOMPLISHED'
                    AND v.cell_id = @cellId
                    AND NOT EXISTS
                    (
                        SELECT 1
                        FROM activity_reports ar
                        WHERE ar.activity_id = a.activity_id
                    )
                    ORDER BY a.activity_date DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Activity activity = new Activity
                            {
                                ActivityId = Convert.ToInt32(reader["activity_id"]),
                                ActivityDate = Convert.ToDateTime(reader["activity_date"]),
                                Objective = reader["objective"].ToString(),
                                Status = (ActivityStatus)Enum.Parse(
                                    typeof(ActivityStatus),
                                    reader["status"].ToString()),
                                VillageId = Convert.ToInt32(reader["village_id"]),
                                ActivityTypeId = Convert.ToInt32(reader["activity_type_id"]),
                                CoordinatorId = Convert.ToInt32(reader["coordinator_id"]),
                                VillageName = reader["village_name"].ToString(),
                                ActivityTypeName = reader["type_name"].ToString()
                            };

                            activities.Add(activity);
                        }
                    }
                }
            }

            return activities;
        }
        public DataTable GetSubmittedReportsByCell(int cellId)
        {
            DataTable reports = new DataTable();

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT
                ar.report_id AS ReportId,
                a.activity_id AS ActivityId,
                v.village_name AS Village,
                at.type_name AS ActivityType,
                a.activity_date AS ActivityDate,
                a.objective AS Objective,
                ar.submission_date AS SubmissionDate,
                ar.attendee_count AS AttendeeCount,
                ar.work_description AS WorkDescription,
                CONCAT(c.first_name, ' ', c.last_name) AS SubmittedBy
            FROM activity_reports ar
            INNER JOIN activities a
                ON ar.activity_id = a.activity_id
            INNER JOIN villages v
                ON a.village_id = v.village_id
            INNER JOIN activity_types at
                ON a.activity_type_id = at.activity_type_id
            INNER JOIN coordinators c
                ON ar.submitted_by_coordinator_id = c.coordinator_id
            WHERE v.cell_id = @cellId
            ORDER BY ar.submission_date DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    using (MySqlDataAdapter adapter =
                        new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(reports);
                    }
                }
            }

            return reports;
        }

        // Save a report only if all business rules are satisfied
        public bool AddReport(
            int activityId,
            int coordinatorId,
            int cellId,
            int attendeeCount,
            string workDescription)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO activity_reports
                    (
                        activity_id,
                        submitted_by_coordinator_id,
                        submission_date,
                        attendee_count,
                        work_description
                    )
                    SELECT
                        a.activity_id,
                        @coordinatorId,
                        NOW(),
                        @attendeeCount,
                        @workDescription
                    FROM activities a
                    INNER JOIN villages v
                        ON a.village_id = v.village_id
                    WHERE a.activity_id = @activityId
                    AND a.status = 'ACCOMPLISHED'
                    AND v.cell_id = @cellId
                    AND EXISTS
                    (
                        SELECT 1
                        FROM coordinators c
                        WHERE c.coordinator_id = @coordinatorId
                        AND c.cell_id = @cellId
                    )
                    AND NOT EXISTS
                    (
                        SELECT 1
                        FROM activity_reports ar
                        WHERE ar.activity_id = a.activity_id
                    )";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@activityId", activityId);
                    cmd.Parameters.AddWithValue("@coordinatorId", coordinatorId);
                    cmd.Parameters.AddWithValue("@cellId", cellId);
                    cmd.Parameters.AddWithValue("@attendeeCount", attendeeCount);
                    cmd.Parameters.AddWithValue("@workDescription", workDescription);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

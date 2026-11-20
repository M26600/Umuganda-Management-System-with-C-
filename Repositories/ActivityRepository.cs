
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UmugandaManagementSystem.Models;

namespace UmugandaManagementSystem.Repositories
{
    public class ActivityRepository
    {
        private DatabaseConnection db = new DatabaseConnection();

        // Get activities belonging to a specific cell
        public List<Activity> GetActivitiesByCell(int cellId)
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
                    WHERE v.cell_id = @cellId
                    ORDER BY a.activity_date DESC";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Activity activity = new Activity
                            {
                                ActivityId = Convert.ToInt32(
                                    reader["activity_id"]),

                                ActivityDate = Convert.ToDateTime(
                                    reader["activity_date"]),

                                Objective = reader["objective"].ToString(),

                                Status = (ActivityStatus)Enum.Parse(
                                    typeof(ActivityStatus),
                                    reader["status"].ToString()),

                                VillageId = Convert.ToInt32(
                                    reader["village_id"]),

                                ActivityTypeId = Convert.ToInt32(
                                    reader["activity_type_id"]),

                                CoordinatorId = Convert.ToInt32(
                                    reader["coordinator_id"]),

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

        // Add a new activity
        public bool AddActivity(
            DateTime activityDate,
            string objective,
            int villageId,
            int activityTypeId,
            int coordinatorId,
            int cellId)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO activities
                    (
                        activity_date,
                        objective,
                        status,
                        village_id,
                        activity_type_id,
                        coordinator_id
                    )
                    SELECT
                        @activityDate,
                        @objective,
                        'PLANNED',
                        v.village_id,
                        @activityTypeId,
                        @coordinatorId
                    FROM villages v
                    WHERE v.village_id = @villageId
                    AND v.cell_id = @cellId
                    AND EXISTS
                    (
                        SELECT 1
                        FROM activity_types
                        WHERE activity_type_id = @activityTypeId
                    )";

                using (MySqlCommand cmd =
                    new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@activityDate", activityDate);

                    cmd.Parameters.AddWithValue(
                        "@objective", objective);

                    cmd.Parameters.AddWithValue(
                        "@villageId", villageId);

                    cmd.Parameters.AddWithValue(
                        "@activityTypeId", activityTypeId);

                    cmd.Parameters.AddWithValue(
                        "@coordinatorId", coordinatorId);

                    cmd.Parameters.AddWithValue(
                        "@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Update an activity's status according to the permitted transitions
        public bool UpdateActivityStatus(
            int activityId,
            ActivityStatus newStatus,
            DateTime newDate,
            int coordinatorId,
            int cellId)
        {
            // A postponed activity must receive a future date when rescheduled
            if (newStatus == ActivityStatus.PLANNED &&
                newDate.Date <= DateTime.Today)
            {
                throw new ArgumentException(
                    "The new activity date must be in the future.");
            }

            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
            UPDATE activities a
            INNER JOIN villages v
                ON a.village_id = v.village_id

            SET
                a.activity_date =
                    CASE
                        WHEN a.status = 'POSTPONED'
                             AND @newStatus = 'PLANNED'
                        THEN @newDate
                        ELSE a.activity_date
                    END,
                a.status = @newStatus

            WHERE a.activity_id = @activityId
            AND v.cell_id = @cellId

            AND EXISTS
            (
                SELECT 1
                FROM coordinators c
                WHERE c.coordinator_id = @coordinatorId
                AND c.cell_id = @cellId
            )

            AND
            (
                (a.status = 'PLANNED'
                    AND @newStatus IN ('ONGOING', 'POSTPONED'))

                OR

                (a.status = 'ONGOING'
                    AND @newStatus IN ('ACCOMPLISHED', 'POSTPONED'))

                OR

                (a.status = 'POSTPONED'
                    AND @newStatus = 'PLANNED')
            )";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@activityId", activityId);

                    cmd.Parameters.AddWithValue(
                        "@newStatus", newStatus.ToString());

                    cmd.Parameters.AddWithValue(
                        "@newDate", newDate);

                    cmd.Parameters.AddWithValue(
                        "@coordinatorId", coordinatorId);

                    cmd.Parameters.AddWithValue(
                        "@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Get activities belonging to a cell using optional filters
        public List<Activity> GetFilteredActivitiesByCell(
            int cellId,
            int? villageId,
            int? activityTypeId,
            DateTime? activityDate,
            ActivityStatus? status)
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
            WHERE v.cell_id = @cellId";

                // Add the village filter only when selected
                if (villageId.HasValue)
                {
                    query += " AND v.village_id = @villageId";
                }

                // Add the activity type filter only when selected
                if (activityTypeId.HasValue)
                {
                    query += " AND a.activity_type_id = @activityTypeId";
                }

                // Add the date filter only when selected
                if (activityDate.HasValue)
                {
                    query += " AND DATE(a.activity_date) = @activityDate";
                }

                // Add the status filter only when selected
                if (status.HasValue)
                {
                    query += " AND a.status = @status";
                }

                query += " ORDER BY a.activity_date DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    if (villageId.HasValue)
                    {
                        cmd.Parameters.AddWithValue(
                            "@villageId", villageId.Value);
                    }

                    if (activityTypeId.HasValue)
                    {
                        cmd.Parameters.AddWithValue(
                            "@activityTypeId", activityTypeId.Value);
                    }

                    if (activityDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue(
                            "@activityDate", activityDate.Value.Date);
                    }

                    if (status.HasValue)
                    {
                        cmd.Parameters.AddWithValue(
                            "@status", status.Value.ToString());
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Activity activity = new Activity
                            {
                                ActivityId = Convert.ToInt32(
                                    reader["activity_id"]),

                                ActivityDate = Convert.ToDateTime(
                                    reader["activity_date"]),

                                Objective = reader["objective"].ToString(),

                                Status = (ActivityStatus)Enum.Parse(
                                    typeof(ActivityStatus),
                                    reader["status"].ToString()),

                                VillageId = Convert.ToInt32(
                                    reader["village_id"]),

                                ActivityTypeId = Convert.ToInt32(
                                    reader["activity_type_id"]),

                                CoordinatorId = Convert.ToInt32(
                                    reader["coordinator_id"]),

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
        // Update an activity that is still PLANNED
        public bool UpdatePlannedActivity(
            int activityId,
            DateTime activityDate,
            string objective,
            int villageId,
            int activityTypeId,
            int coordinatorId,
            int cellId)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
            UPDATE activities a
            INNER JOIN villages currentVillage
                ON a.village_id = currentVillage.village_id
            INNER JOIN villages newVillage
                ON newVillage.village_id = @villageId
                AND newVillage.cell_id = @cellId

            SET
                a.activity_date = @activityDate,
                a.objective = @objective,
                a.village_id = @villageId,
                a.activity_type_id = @activityTypeId

            WHERE a.activity_id = @activityId
            AND a.status = 'PLANNED'
            AND currentVillage.cell_id = @cellId

            AND EXISTS
            (
                SELECT 1
                FROM coordinators c
                WHERE c.coordinator_id = @coordinatorId
                AND c.cell_id = @cellId
            )

            AND EXISTS
            (
                SELECT 1
                FROM activity_types
                WHERE activity_type_id = @activityTypeId
            )";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@activityDate", activityDate);
                    cmd.Parameters.AddWithValue("@objective", objective);
                    cmd.Parameters.AddWithValue("@villageId", villageId);
                    cmd.Parameters.AddWithValue("@activityTypeId", activityTypeId);
                    cmd.Parameters.AddWithValue("@activityId", activityId);
                    cmd.Parameters.AddWithValue("@coordinatorId", coordinatorId);
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Delete an activity only if it is still PLANNED
        public bool DeletePlannedActivity(int activityId, int cellId)
        {
            using (MySqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"
            DELETE a
            FROM activities a
            INNER JOIN villages v
                ON a.village_id = v.village_id
            WHERE a.activity_id = @activityId
            AND a.status = 'PLANNED'
            AND v.cell_id = @cellId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@activityId", activityId);
                    cmd.Parameters.AddWithValue("@cellId", cellId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }


}

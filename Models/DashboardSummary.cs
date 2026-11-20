using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmugandaManagementSystem.Models
{
    public class DashboardSummary
    {
        public int TotalVillages { get; set; }
        public int TotalActivities { get; set; }

        public int PlannedActivities { get; set; }
        public int OngoingActivities { get; set; }
        public int AccomplishedActivities { get; set; }
        public int PostponedActivities { get; set; }

        public int UpcomingPlannedActivities { get; set; }
        public int SubmittedReports { get; set; }
    }
}

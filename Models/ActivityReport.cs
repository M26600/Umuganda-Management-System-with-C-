using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmugandaManagementSystem.Models
{
    public class ActivityReport
    {
        public int ReportId { get; set; }
        public int ActivityId { get; set; }
        public int SubmittedByCoordinatorId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int AttendeeCount { get; set; }
        public string WorkDescription { get; set; }
    }
}

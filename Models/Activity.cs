using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmugandaManagementSystem.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Objective { get; set; }
        public ActivityStatus Status { get; set; }

        public int VillageId { get; set; }
        public int ActivityTypeId { get; set; }
        public int CoordinatorId { get; set; }
        public string VillageName { get; set; }
        public string ActivityTypeName { get; set; }
    }
}

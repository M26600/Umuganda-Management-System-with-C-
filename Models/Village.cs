using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmugandaManagementSystem.Models
{
    public class Village
    {
        public int VillageId { get; set; }
        public string VillageName { get; set; }
        public string VillageCode { get; set; }
        public string LeaderName { get; set; }
        public int CellId { get; set; }
    }
}

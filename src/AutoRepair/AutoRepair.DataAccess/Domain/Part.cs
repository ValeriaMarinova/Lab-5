using System;
using System.Collections.Generic;

namespace AutoRepair.DataAccess.Domain
{
    public partial class Part
    {
        public Part()
        {
            RepairItems = new HashSet<RepairItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual ICollection<RepairItem> RepairItems { get; set; }
    }
}

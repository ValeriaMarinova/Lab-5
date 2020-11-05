using System;
using System.Collections.Generic;

namespace AutoRepair.DataAccess.Domain
{
    public partial class Order
    {
        public Order()
        {
            RepairItems = new HashSet<RepairItem>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
        public int WorkerId { get; set; }
        public string ProblemDescription { get; set; }
        public string Solution { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual ICollection<RepairItem> RepairItems { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace AutoRepair.DataAccess.Domain
{
    public partial class RepairItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PartId { get; set; }
        public int Qty { get; set; }

        public virtual Order Order { get; set; }
        public virtual Part Part { get; set; }
    }
}

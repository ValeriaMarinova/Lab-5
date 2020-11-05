using System;
using System.Collections.Generic;

namespace AutoRepair.DataAccess.Domain
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegistrationPlate { get; set; }
        public int? Year { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

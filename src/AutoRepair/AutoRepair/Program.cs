using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Services;
using System;

namespace AutoRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AutoRepairContext())
            {
                CustomersService customersService = new CustomersService(context);
                customersService.Create("Hedy Greene", "Ap #696-3279 Viverra. Avenue Latrobe DE 38100");
                customersService.Create("Joan Romero", "Lacinia Avenue Idaho Falls Ohio");
                customersService.Create("Davis Patrick", "2546 Sociosqu Rd. Bethlehem Utah");
            }
        }
    }
}

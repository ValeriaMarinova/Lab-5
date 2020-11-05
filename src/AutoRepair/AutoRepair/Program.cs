using AutoRepair.DataAccess.Context;
using System;

namespace AutoRepair
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AutoRepairContext()) { }
        }
    }
}

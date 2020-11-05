using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Services.Interfaces;
using System;

namespace AutoRepair.DataAccess.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AutoRepairContext _context;

        public OrdersService(AutoRepairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}

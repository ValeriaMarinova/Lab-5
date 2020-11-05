using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services.Interfaces;
using System;
using System.Linq;

namespace AutoRepair.DataAccess.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AutoRepairContext _context;

        public OrdersService(AutoRepairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddPart(Order order, Part part, int quantity)
        {
            var item = _context.RepairItems.Add(new RepairItem() { Part = part, Qty = quantity });

            order.RepairItems.Add(item.Entity);
        }

        public void AddRepairSummary(Order order, string repairResultDescription = "Done")
        {
            order.Solution = repairResultDescription;
        }

        public void AssingWorker(Order order, Worker worker)
        {
            order.Worker = worker;
        }

        public double GetTotalCost(Order order)
        {
            var total = order.RepairItems.Sum(x => x.Part.Price * x.Qty);
            return total;
        }

        public Order OpenOrder(Customer customer, Vehicle vehicle, string problemDescription)
        {
            var newOrder = new Order()
            {
                Customer = customer,
                Vehicle = vehicle,
                ProblemDescription = problemDescription
            };

            _context.Orders.Add(newOrder);

            return newOrder;
        }

        public void SaveOrder(Order order)
        {
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.RepairItems.RemoveRange(order.RepairItems);
            _context.Orders.Remove(order);

            _context.SaveChanges();
        }
    }
}

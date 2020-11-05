using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services.Interfaces;
using System;
using System.Linq;

namespace AutoRepair.DataAccess.Services
{
    public class WorkersService : IWorkersService
    {
        private readonly AutoRepairContext _context;

        public WorkersService(AutoRepairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(string name, string position)
        {
            var worker = new Worker() { Name = name, Position = position };

            _context.Workers.Add(worker);
            _context.SaveChanges();
        }

        public void Delete(string name)
        {
            var worker = _context.Workers.FirstOrDefault(c => c.Name == name);

            if (worker != null)
            {
                _context.Workers.Remove(worker);
                _context.SaveChanges();
            }
        }

        public Worker Get(string name)
        {
            return _context.Workers.FirstOrDefault(c => c.Name == name);
        }
    }
}

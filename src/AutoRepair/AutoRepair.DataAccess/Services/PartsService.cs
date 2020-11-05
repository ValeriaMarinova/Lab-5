using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services.Interfaces;
using System;
using System.Linq;

namespace AutoRepair.DataAccess.Services
{
    public class PartsService : IPartsService
    {
        private readonly AutoRepairContext _context;

        public PartsService(AutoRepairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(string name, int price)
        {
            var part = new Part() { Name = name, Price = price };

            _context.Parts.Add(part);
            _context.SaveChanges();
        }

        public void Delete(string name)
        {
            var part = _context.Parts.FirstOrDefault(x => x.Name == name);

            if (part != null)
            {
                _context.Parts.Remove(part);
                _context.SaveChanges();
            }
        }

        public Part Get(string name)
        {
            return _context.Parts.FirstOrDefault(x => x.Name == name);
        }
    }
}

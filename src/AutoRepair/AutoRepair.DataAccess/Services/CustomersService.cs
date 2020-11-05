using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services.Interfaces;
using System;
using System.Linq;

namespace AutoRepair.DataAccess.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly AutoRepairContext _context;

        public CustomersService(AutoRepairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(string name, string address)
        {
            var customer = new Customer() { Name = name, Address = address };

            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Delete(string name)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Name == name);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public Customer Get(string name)
        {
            return _context.Customers.FirstOrDefault(x => x.Name == name);
        }

        public void DeleteAllByName(string pattern)
        {
            var customers = _context.Customers.Where(x => x.Name.Contains(pattern));

            _context.RemoveRange(customers);
            _context.SaveChanges();
        }
    }
}

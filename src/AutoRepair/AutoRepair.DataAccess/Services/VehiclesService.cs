using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services.Interfaces;
using System;
using System.Linq;

namespace AutoRepair.DataAccess.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly AutoRepairContext _context;

        public VehiclesService(AutoRepairContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(string make, string model, string registrationPlate, int year)
        {
            var vehicle = new Vehicle()
            {
                Make = make,
                Model = model,
                RegistrationPlate = registrationPlate,
                Year = year
            };

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void Delete(string registrationPlate)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(c => c.RegistrationPlate == registrationPlate);

            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                _context.SaveChanges();
            }
        }

        public Vehicle Get(string registrationPlate)
        {
            return _context.Vehicles.FirstOrDefault(c => c.RegistrationPlate == registrationPlate);
        }
    }
}

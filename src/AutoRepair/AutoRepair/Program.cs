using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace AutoRepair
{
    class Program
    {
        static IConfiguration configuration;

        static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var connectionString = configuration.GetConnectionString("PostgresAutorepairDB");

            //CRUD Examples
            CRUDExampleOnCustomers(connectionString);
            CRUDExampleOnParts(connectionString);
            CRUDExampleOnVehicles(connectionString);
            CRUDExampleOnWorkers(connectionString);

            CRUDExampleOnPartsMeasured(connectionString);
        }

        #region CRUD examples

        static void CRUDExampleOnCustomers(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                context.Customers.Add(new Customer() { Name = "Johnnie Doe", Address = "New York" }); //Create

                var customer = context.Customers.First(x => x.Name.Contains("Doe")); //Read

                customer.Name = "John Doe";
                context.Update(customer); //Update

                context.Remove(customer); //Delete
            }
        }

        static void CRUDExampleOnParts(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                context.Parts.Add(new Part() { Name = "Gear", Price = 100 }); //Create

                var part = context.Parts.First(x => x.Name == "Gear"); //Read

                part.Price = 150;
                context.Update(part); //Update

                context.Remove(part); //Delete
            }
        }

        static void CRUDExampleOnVehicles(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                context.Vehicles.Add(new Vehicle() { Make = "Chevrolet", Model = "Silverado", RegistrationPlate = "GHT430", Year = 2003 }); //Create

                var vehicle = context.Vehicles.First(x => x.Make == "Chevrolet" && x.Year == 2003);

                vehicle.Year = 2004;
                context.Update(vehicle); //Update

                context.Remove(vehicle); //Delete
            }
        }

        static void CRUDExampleOnWorkers(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                context.Workers.Add(new Worker() { Name = "Edward Nieves", Position = "Mechanic" }); //Create

                var worker = context.Workers.First(x => x.Name.StartsWith("Edward"));

                worker.Position = "Sr. Mechanic";
                context.Update(worker); //Update

                context.Remove(worker); //Delete
            }
        }
        #endregion

        static void CRUDExampleOnPartsMeasured(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                var createTime = MeasureHelper.MeasureMilliseconds(() =>
                {
                    context.Parts.Add(new Part() { Name = "Gear", Price = 100 }); //Create
                });
                context.SaveChanges();

                Part part = null;
                var readTime = MeasureHelper.MeasureMilliseconds(() =>
                {
                    part = context.Parts.First(x => x.Name == "Gear"); //Read
                });

                var updateTime = MeasureHelper.MeasureMilliseconds(() =>
                {
                    part.Price = 150;
                    context.Update(part); //Update
                });

                var deleteTime = MeasureHelper.MeasureMilliseconds(() =>
                {
                    context.Remove(part); //Delete
                });
            }
        }
    }
}

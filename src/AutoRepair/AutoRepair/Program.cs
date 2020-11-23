using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

            //Прогрев контекста
              using (var context = new AutoRepairContext(connectionString)) { context.Customers.FirstOrDefault(); }

            //Эти запросы пересоздадут базу, заполнят таблицы тестовыми данными
            //Перед началом демонтрации программы, выполнить ТОЛЬКО их и закомментировать
            // Scenarios.ResetAndFillDatabase(connectionString);
            // Scenarios.ExampleOfOrderWorkflow(connectionString);

            //Запросы с замером времени
             Scenarios.GetTheOldestCarMeasured(connectionString);
             Scenarios.GetTotalCostMeasured(connectionString);
             Scenarios.GetFullOrderInformationMeasured(connectionString);

            //  Pagination example

            var pageSize = configuration.GetValue<int>("PageSize");
            using (var context = new AutoRepairContext(connectionString))
            {


                int itemsCount = context.Workers.Count(); //количество строк в таблице
                int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);


                for (int pageNumber = 1; pageNumber <= pagesCount; pageNumber++)
                {
                    var itemsOnPage = GetWorkersPage(context, pageNumber, pageSize); //получим элементы со страницы pageNumber
                    Console.WriteLine($"Page {pageNumber}: " + string.Join(", ", itemsOnPage.Select(x => x.Name))); //выведем только имена
                }
            }

            //CRUD Examples
            //CRUDExampleOnCustomers(connectionString);
            //CRUDExampleOnParts(connectionString);
            //CRUDExampleOnVehicles(connectionString);
            //CRUDExampleOnWorkers(connectionString);
            Console.ReadLine();
        }

        #region Pagination

        static IEnumerable<Worker> GetWorkersPage(AutoRepairContext context, int page, int pageSize)
        {
            var count = context.Workers.Count();
            var items = context.Workers.Skip((page - 1) * pageSize).Take(pageSize);
            return items.ToList();
        }

        static IEnumerable<Vehicle> GetVehiclesPage(AutoRepairContext context, int page, int pageSize)
        {
            var count = context.Vehicles.Count();
            var items = context.Vehicles.Skip((page - 1) * pageSize).Take(pageSize);
            return items.ToList();
        }
        static IEnumerable<Customer> GetCustomersPage(AutoRepairContext context, int page, int pageSize)
        {
            var count = context.Customers.Count();
            var items = context.Customers.Skip((page - 1) * pageSize).Take(pageSize);
            return items.ToList();
        }
        static IEnumerable<Order> GetOrdersPage(AutoRepairContext context, int page, int pageSize)
        {
            var count = context.Orders.Count();
            var items = context.Orders.Skip((page - 1) * pageSize).Take(pageSize);
            return items.ToList();
        }
        static IEnumerable<RepairItem> GetRepairItemsPage(AutoRepairContext context, int page, int pageSize)
        {
            var count = context.RepairItems.Count();
            var items = context.RepairItems.Skip((page - 1) * pageSize).Take(pageSize);
            return items.ToList();
        }
        #endregion

        #region CRUD

        static void CRUDExampleOnParts(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                context.Parts.Add(new Part() { Name = "Gear", Price = 100 }); //Create
                context.SaveChanges();
                Console.WriteLine("Запись создана");
                Console.ReadLine();

                var part = context.Parts.First(x => x.Name == "Gear"); //Read
                Console.WriteLine($"Запись получена: {part.Id} {part.Name} {part.Price}");
                Console.ReadLine();

                part.Price = 150;
                context.Update(part); //Update
                context.SaveChanges();
                Console.WriteLine("Запись обновлена");
                Console.ReadLine();

                context.Remove(part); //Delete
                context.SaveChanges();
                Console.WriteLine("Запись удалена");
                Console.ReadLine();
            }
        }

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
    }
}

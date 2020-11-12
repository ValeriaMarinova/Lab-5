using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services;
using AutoRepair.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace AutoRepair
{
    public static class Scenarios
    {
        /// <summary>
        /// Пересоздает базу (удаляет и создает заново), заполняет её тестовыми данными
        /// </summary>
        public static void ResetAndFillDatabase(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString, resetDatabase: true))
            {
                //Заполняем заказчиков
                context.Customers.Add(new Customer() { Name = "Hedy Greene", Address = "Ap #696-3279 Viverra. Avenue Latrobe DE 38100" });
                context.Customers.Add(new Customer() { Name = "Joan Romero", Address = "Lacinia Avenue Idaho Falls Ohio" });
                context.Customers.Add(new Customer() { Name = "Davis Patrick", Address = "2546 Sociosqu Rd. Bethlehem Utah" });

                //Заполняем машины
                context.Vehicles.Add(new Vehicle() { Make = "Ford", Model = "F-Series", RegistrationPlate = "798 PAK", Year = 2007 });
                context.Vehicles.Add(new Vehicle() { Make = "Chevrolet", Model = "Silverado", RegistrationPlate = "GHT430", Year = 2003 });
                context.Vehicles.Add(new Vehicle() { Make = "Suzuki", Model = "Aerio", RegistrationPlate = "50D24H8", Year = 2011 });

                //Заполняем список запчастей
                context.Parts.Add(new Part() { Name = "Front Clip", Price = 5 });
                context.Parts.Add(new Part() { Name = "Airbag sensor", Price = 10 });
                context.Parts.Add(new Part() { Name = "Starter Pinion Gear", Price = 50 });
                context.Parts.Add(new Part() { Name = "Differential", Price = 1500 });
                context.Parts.Add(new Part() { Name = "Backup Camera", Price = 115 });
                context.Parts.Add(new Part() { Name = "Door Contact", Price = 75 });

                //Заполняем механиков
                context.Workers.Add(new Worker() { Name = "Leilani Boyer", Position = "Mechanic" });
                context.Workers.Add(new Worker() { Name = "Edward Nieves", Position = "Mechanic" });
                context.Workers.Add(new Worker() { Name = "Christian Emerson", Position = "St. Mechanic" });
                context.Workers.Add(new Worker() { Name = "Raymond Levy", Position = "Jr. Mechanic" });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Пример заполнения всех данных для Заказа на ремонт
        /// </summary>
        public static void ExampleOfOrderWorkflow(string connectionString)
        {
            using (var context = new AutoRepairContext(connectionString))
            {
                //Берем заказчика
                var customer = context.Customers.First(x => x.Name == "Joan Romero");

                //Берем автомобиль
                var vehicle = context.Vehicles.First(x => x.RegistrationPlate == "GHT430");

                //Создаем заказ на ремонт автомобиля
                IOrdersService ordersService = new OrdersService(context);
                var order = ordersService.OpenOrder(customer, vehicle, "Check engine light is blinking. Please check.");

                //Назначаем первого подходящего мастера нужного уровня на заказ
                var mechanic = context.Workers.First(x => x.Position == "Mechanic");
                ordersService.AssingWorker(order, mechanic);

                //Выбираем детали для ремонта
                var starterGear = context.Parts.First(x => x.Name == "Starter Pinion Gear");
                var differential = context.Parts.First(x => x.Name == "Differential");
                var clip = context.Parts.First(x => x.Name == "Front Clip");

                //Добавляем в заказ необходимое количество деталей
                ordersService.AddPart(order, differential, 1);
                ordersService.AddPart(order, starterGear, 2);
                ordersService.AddPart(order, clip, 4);

                //Записываем результат работы
                ordersService.AddRepairSummary(order, "Change differential. All is ok now.");

                //Закрываем заказ
                ordersService.SaveOrder(order);

                //Для того, чтобы перезапускать пример, удалим сохраненные данные
                //ordersService.DeleteOrder(order);
            }
        }

        #region Сценарии для замера выполнения запросов

        /// <summary>
        /// Запрос всех данных по заказу из связанных таблиц
        /// </summary>
        public static void GetFullOrderInformationMeasured(string connectionString, out long elapsedTime)
        {
            Stopwatch stopwatch = new Stopwatch();

            using (var context = new AutoRepairContext(connectionString))
            {
                stopwatch.Start();

                var order = context.Orders
                    .Include(order => order.Customer)
                    .Include(order => order.RepairItems)
                    .Include(order => order.Vehicle)
                    .Include(order => order.Worker)
                    .First();

                stopwatch.Stop();
                elapsedTime = stopwatch.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// Запрос стоимости всех запчастей для ремонта
        /// </summary>
        public static void GetTotalCostMeasured(string connectionString, out long elapsedTime)
        {
            Stopwatch stopwatch = new Stopwatch();

            using (var context = new AutoRepairContext(connectionString))
            {
                stopwatch.Start();

                var order = context.Orders
                    .Include(order => order.RepairItems)
                    .ThenInclude(item => item.Part)
                    .First();

                var sum = order.RepairItems.Sum(x => x.Part.Price * x.Qty);

                stopwatch.Stop();
                elapsedTime = stopwatch.ElapsedMilliseconds;
            }
        }
        #endregion
    }
}

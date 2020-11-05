using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Domain;
using AutoRepair.DataAccess.Services;
using AutoRepair.DataAccess.Services.Interfaces;
using System.Linq;

namespace AutoRepair
{
    class Scenarios
    {
        public static void FillDatabase()
        {
            using (var context = new AutoRepairContext(resetDatabase: true))
            {
                //Заполняем заказчиков
                ICustomersService customersService = new CustomersService(context);
                customersService.Create("Hedy Greene", "Ap #696-3279 Viverra. Avenue Latrobe DE 38100");
                customersService.Create("Joan Romero", "Lacinia Avenue Idaho Falls Ohio");
                customersService.Create("Davis Patrick", "2546 Sociosqu Rd. Bethlehem Utah");

                //Заполняем машины
                IVehiclesService vehiclesService = new VehiclesService(context);
                vehiclesService.Create("Ford", "F-Series", "798 PAK", 2007);
                vehiclesService.Create("Chevrolet", "Silverado", "GHT430", 2003);
                vehiclesService.Create("Suzuki", "Aerio", "50D24H8", 2011);
                
                //Заполняем список запчастей
                IPartsService partsService = new PartsService(context);
                partsService.Create("Front Clip", 5);
                partsService.Create("Airbag sensor", 10);
                partsService.Create("Starter Pinion Gear", 50);
                partsService.Create("Differential", 1500);
                partsService.Create("Backup Camera", 115);
                partsService.Create("Door Contact", 75);

                //Заполняем механиков
                IWorkersService workersService = new WorkersService(context);
                workersService.Create("Leilani Boyer", "Mechanic");
                workersService.Create("Edward Nieves", "Mechanic");
                workersService.Create("Christian Emerson", "St. Mechanic");
                workersService.Create("Raymond Levy", "Jr. Mechanic");
            }
        }

        public static void ExampleOfOrderWorkflow()
        {
            using (var context = new AutoRepairContext())
            {
                //Берем заказчика
                ICustomersService customersService = new CustomersService(context);
                var customer = customersService.Get("Joan Romero");

                //Берем автомобиль
                IVehiclesService vehiclesService = new VehiclesService(context);
                var vehicle = vehiclesService.Get("GHT430");

                //Создаем заказ на ремонт автомобиля
                IOrdersService ordersService = new OrdersService(context);
                var order = ordersService.OpenOrder(customer, vehicle, "Check engine light is blinking. Please check.");

                //Назначаем первого подходящего мастера нужного уровня на заказ
                IWorkersService workersService = new WorkersService(context);
                var mechanic = workersService.GetAllByPosition("Mechanic").First();
                ordersService.AssingWorker(order, mechanic);

                //Выбираем детали для ремонта
                IPartsService partsService = new PartsService(context);
                var starterGear = partsService.Get("Starter Pinion Gear");
                var differential = partsService.Get("Differential");
                var clip = partsService.Get("Front Clip");

                //Добавляем в заказ необходимое количество деталей
                ordersService.AddPart(order, differential, 1);
                ordersService.AddPart(order, starterGear, 2);
                ordersService.AddPart(order, clip, 4);

                //Записываем результат работы
                ordersService.AddRepairSummary(order, "Change differential. All is ok now.");

                //Закрываем заказ
                ordersService.SaveOrder(order);

                //Для того, чтобы перезапускать пример, удалим сохраненные данные
                ordersService.DeleteOrder(order);
            }
        }
    }
}

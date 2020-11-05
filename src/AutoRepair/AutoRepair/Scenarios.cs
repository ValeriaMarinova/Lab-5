using AutoRepair.DataAccess.Context;
using AutoRepair.DataAccess.Services;
using AutoRepair.DataAccess.Services.Interfaces;

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
    }
}

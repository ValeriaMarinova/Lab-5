using AutoRepair.DataAccess.Domain;

namespace AutoRepair.DataAccess.Services.Interfaces
{
    public interface IOrdersService
    {
        Order OpenOrder(Customer customer, Vehicle vehicle, string problemDescription = "Diagnostic");
        void AssingWorker(Order order, Worker worker);
        void AddPart(Order order, Part part, int quantity);
        void AddRepairSummary(Order order, string repairResultDescription = "Done");
        double GetTotalCost(Order order);
        void SaveOrder(Order order);
        void DeleteOrder(Order order);
    }
}

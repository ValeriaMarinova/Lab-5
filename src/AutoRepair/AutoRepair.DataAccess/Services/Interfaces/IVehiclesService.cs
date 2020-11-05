using AutoRepair.DataAccess.Domain;

namespace AutoRepair.DataAccess.Services.Interfaces
{
    public interface IVehiclesService
    {
        void Create(string make, string model, string registrationPlate, int year);
        Vehicle Get(string registrationPlate);
        void Delete(string registrationPlate);
    }
}

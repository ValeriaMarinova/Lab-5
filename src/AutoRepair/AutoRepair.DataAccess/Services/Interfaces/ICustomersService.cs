using AutoRepair.DataAccess.Domain;

namespace AutoRepair.DataAccess.Services.Interfaces
{
    public interface ICustomersService
    {
        void Create(string name, string address);
        Customer Get(string name);
        void Delete(string name);
    }
}

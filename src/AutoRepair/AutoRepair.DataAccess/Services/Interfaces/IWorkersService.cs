using AutoRepair.DataAccess.Domain;

namespace AutoRepair.DataAccess.Services.Interfaces
{
    public interface IWorkersService
    {
        void Create(string name, string position);
        Worker Get(string name);
        void Delete(string name);
    }
}

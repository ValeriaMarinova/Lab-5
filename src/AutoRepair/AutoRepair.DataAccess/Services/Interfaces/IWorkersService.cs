using AutoRepair.DataAccess.Domain;
using System.Collections.Generic;

namespace AutoRepair.DataAccess.Services.Interfaces
{
    public interface IWorkersService
    {
        void Create(string name, string position);
        Worker Get(string name);
        ICollection<Worker> GetAllByPosition(string position);
        void Delete(string name);
    }
}

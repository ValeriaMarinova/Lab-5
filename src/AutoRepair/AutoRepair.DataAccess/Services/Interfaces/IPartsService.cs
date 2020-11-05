using AutoRepair.DataAccess.Domain;

namespace AutoRepair.DataAccess.Services.Interfaces
{
    public interface IPartsService
    {
        void Create(string name, int price);
        Part Get(string name);
        void Delete(string name);
    }
}

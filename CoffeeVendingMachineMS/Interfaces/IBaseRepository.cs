using MongoDB.Driver;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface IBaseRepository
    {
        public IMongoDatabase Database();
    }
}

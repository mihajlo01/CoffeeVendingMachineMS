using CoffeeVendingMachineMS.Interfaces;
using MongoDB.Driver;

namespace CoffeeVendingMachineMS.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public IMongoDatabase Database()
        {
            var client = new MongoClient("mongodb+srv://mihajlo:C0ffee1!@coffeevendingmachine.av7ngcv.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("CoffeeVendingMachine");

            return database;
        }
    }
}

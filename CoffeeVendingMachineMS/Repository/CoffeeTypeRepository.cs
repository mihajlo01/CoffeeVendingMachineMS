using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Repositories
{
    public class CoffeeTypeRepository : ICoffeeTypeRepository
    {
        private readonly IBaseRepository baseRepository;

        public CoffeeTypeRepository()
        {
            baseRepository = new MongoDbRepository();
        }

        public async Task<IEnumerable<CoffeeType>> GetCoffeeTypes()
        {
            return await baseRepository.Database().GetCollection<CoffeeType>("CoffeeType").Find(x => x.Code > 0).ToListAsync();
        }

        public long GetCoffeeTypesCount()
        {
            return baseRepository.Database().GetCollection<CoffeeType>("CoffeeType").CountDocuments(x => x.Code > 0);
        }
    }
}

using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Repositories
{
    public class CoffeeTypeBusinessLogic : ICoffeeTypeRepository
    {
        private readonly IBaseRepository baseRepository;

        public CoffeeTypeBusinessLogic()
        {
            baseRepository = new MongoDbBusinessLogic();
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

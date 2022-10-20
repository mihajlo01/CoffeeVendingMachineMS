using CoffeeVendingMachineMS.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface IBaseRepository
    {
        public IMongoDatabase Database();
    }
}

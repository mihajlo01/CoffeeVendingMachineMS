using CoffeeVendingMachineMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface ICoffeeTypeRepository
    {
        public Task<IEnumerable<CoffeeType>> GetCoffeeTypes();
        public long GetCoffeeTypesCount();
    }
}

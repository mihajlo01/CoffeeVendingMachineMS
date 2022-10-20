using CoffeeVendingMachineMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface ICoffeeTypeBusinessLogic
    {
        public Task<IEnumerable<CoffeeType>> GetCoffeeTypes();
        public long GetCoffeeTypesCount();
    }
}

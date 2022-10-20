using CoffeeVendingMachineMS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface IMoneyBusinessLogic
    {
        public CashCodes CheckAndUpdateInsertedCash(string inserted);
        public CashCodes CheckOrderPrice(string orderPrice);
    }
}

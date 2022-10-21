using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface IMoneyBusinessLogic
    {
        decimal Balance { get; set; }
        decimal OrderTotal { get; set; }
        public CashCodes CheckAndUpdateBalance(string inserted);
        public CashCodes UpdateOrderPrice(decimal orderPrice);
    }
}

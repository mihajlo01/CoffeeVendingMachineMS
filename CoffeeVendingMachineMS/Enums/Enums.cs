using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Enums
{
    public enum CashCodes
    {
        WrongInput = -2,
        BelowMinimum = -1,
        NotEnoughMoney = 0,
        AcceptableAmount = 1,
        ShouldReturn = 2
    }
}

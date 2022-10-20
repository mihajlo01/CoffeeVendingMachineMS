using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Enums
{
    public enum CashCodes
    {
        NotInUseCode = -4,
        WrongInput = -3,
        BelowMinimum = -2,
        NotValid = -1,
        NotEnoughMoney = 0,
        AcceptableAmount = 1,
        ShouldReturn = 2
    }
}

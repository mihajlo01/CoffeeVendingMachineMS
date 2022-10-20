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

    public enum AddonTypes
    {
        None = 0,
        Sugar = 1,
        Milk = 2,
        Cream = 3,
        Caramelle = 4
    }

    public enum SugarAddon
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3
    }
    public enum BaseAddon
    {
        None = 0,
        One = 1
    }
}

using CoffeeVendingMachineMS.Enums;

namespace CoffeeVendingMachineMS.Interfaces
{
    public interface IMoneyRepository
    {
        decimal Balance { get; set; }
        decimal OrderTotal { get; set; }
        public CashCodes CheckAndUpdateBalance(string inserted);
        public CashCodes UpdateOrderPrice(decimal orderPrice);
    }
}

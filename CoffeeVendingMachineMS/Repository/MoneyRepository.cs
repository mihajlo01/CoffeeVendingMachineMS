using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeVendingMachineMS.BusinessLogic
{
    public class MoneyRepository : IMoneyRepository
    {
        private readonly ICoffeeTypeRepository coffeeTypeRepository;

        private decimal _balance;
        public decimal Balance
        {
            get => _balance;
            set => _balance = value;
        }

        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get => _orderTotal;
            set => _orderTotal = value;
        }

        private decimal OrderPrice;

        public MoneyRepository()
        {
            coffeeTypeRepository = new CoffeeTypeRepository();
        }

        public CashCodes CheckAndUpdateBalance(string inserted)
        {
            List<decimal> allowedCoins = new List<decimal> { 0.01m, 0.02m, 0.05m, 0.10m, 0.20m, 0.50m, 1, 2 };
            decimal insertedCash;

            if(!Decimal.TryParse(inserted, out insertedCash))
            {
                if (insertedCash > 2 || (!allowedCoins.Contains(insertedCash) && insertedCash != 0) || inserted.Contains("-"))
                {
                    return CashCodes.NotValid;
                }

                var minimumValue = coffeeTypeRepository.GetCoffeeTypes().Result.OrderByDescending(x => x.Price).LastOrDefault().Price;
                if (Balance < minimumValue)
                {
                    return CashCodes.BelowMinimum;
                }

                return CashCodes.WrongInput;
            }
            else
            {
                if(insertedCash > 2 || !allowedCoins.Contains(insertedCash) || inserted.Contains("-"))
                {
                    return CashCodes.NotValid;
                }

                Balance += insertedCash;
                return CashCodes.AcceptableAmount;
            }
        }

        public CashCodes UpdateOrderPrice(decimal orderPrice)
        {
            OrderPrice = Convert.ToDecimal(orderPrice);

            if (OrderPrice > Balance)
            {
                return CashCodes.NotEnoughMoney;
            }
            else
            {
                OrderTotal += OrderPrice;
                Balance -= OrderPrice;
                return CashCodes.AcceptableAmount;
            }
        }
    }
}

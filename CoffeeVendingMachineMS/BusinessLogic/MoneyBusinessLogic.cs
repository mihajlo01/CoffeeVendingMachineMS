using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.BusinessLogic
{
    public class MoneyBusinessLogic : IMoneyBusinessLogic
    {
        private readonly ICoffeeTypeBusinessLogic coffeeTypeBusinessLogic;

        private decimal _ballance;
        public decimal Ballance
        {
            get => _ballance;
            set => _ballance = value;
        }

        private decimal _orderTotal;
        public decimal OrderTotal
        {
            get => _orderTotal;
            set => _orderTotal = value;
        }

        private decimal OrderPrice;

        public MoneyBusinessLogic()
        {
            coffeeTypeBusinessLogic = new CoffeeTypeBusinessLogic();
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

                var minimumValue = coffeeTypeBusinessLogic.GetCoffeeTypes().Result.OrderByDescending(x => x.Price).LastOrDefault().Price;
                if (Ballance < minimumValue)
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

                Ballance += insertedCash;
                return CashCodes.AcceptableAmount;
            }
        }

        public CashCodes UpdateInitialOrderPrice(decimal orderPrice)
        {
            OrderPrice = Convert.ToDecimal(orderPrice);

            if (OrderPrice > Ballance)
            {
                return CashCodes.NotEnoughMoney;
            }
            else
            {
                OrderTotal = OrderPrice;
                Ballance -= OrderPrice;
                return CashCodes.AcceptableAmount;
            }
        }
    }
}

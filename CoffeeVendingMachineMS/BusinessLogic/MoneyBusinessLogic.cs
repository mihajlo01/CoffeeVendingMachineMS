using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.BusinessLogic
{
    public class MoneyBusinessLogic : IMoneyBusinessLogic
    {
        private readonly ICoffeeTypeBusinessLogic coffeeTypeBusinessLogic;
        public decimal ballance;
        public decimal orderTotal;
        private decimal orderPrice;

        public MoneyBusinessLogic()
        {
            coffeeTypeBusinessLogic = new CoffeeTypeBusinessLogic();
        }

        public CashCodes CheckAndUpdateBalance(string inserted)
        {
            decimal insertedCash;

            if(!Decimal.TryParse(inserted, out insertedCash))
            {
                var minimumValue = coffeeTypeBusinessLogic.GetCoffeeTypes().Result.OrderByDescending(x => x.Price).FirstOrDefault().Price;
                if (ballance < minimumValue)
                {
                    return CashCodes.BelowMinimum;
                }

                return CashCodes.WrongInput;
            }
            else
            {
                ballance += insertedCash;
                return CashCodes.AcceptableAmount;
            }
        }

        public CashCodes CheckOrderPrice(string stringOrderPrice)
        {
            if (!Decimal.TryParse(stringOrderPrice, out orderPrice))
            {
                return CashCodes.WrongInput;
            }

            if (orderPrice > ballance)
            {
                return CashCodes.NotEnoughMoney;
            }
            else
            {
                orderTotal = orderPrice;
                return CashCodes.AcceptableAmount;
            }
        }
    }
}

using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.BusinessLogic
{
    public class MoneyBusinessLogic
    {
        private readonly ICoffeeTypeRepository coffeeTypeRepository;
        public decimal ballance;
        public decimal orderTotal;
        private decimal balanceToCheck;

        public MoneyBusinessLogic(ICoffeeTypeRepository coffeeTypeRepository) : base()
        {
            this.coffeeTypeRepository = coffeeTypeRepository;
        }

        public CashCodes CheckAndUpdateBalance(string inserted)
        {
            decimal insertedCash;

            if(!Decimal.TryParse(inserted, out insertedCash))
            {
                return CashCodes.WrongInput;
            }
            else
            {
                var minimumValue = coffeeTypeRepository.GetCoffeeTypes().Result.OrderByDescending(x => x.Price).FirstOrDefault().Price;

                if(ballance < minimumValue)
                {
                    return CashCodes.BelowMinimum;
                }

                ballance += insertedCash;
                return CashCodes.AcceptableAmount;
            }
        }

        public CashCodes CheckOrderPrice(string orderPrice)
        {
            if (!Decimal.TryParse(orderPrice, out balanceToCheck))
            {
                return CashCodes.WrongInput;
            }

            if (balanceToCheck > ballance)
            {
                return CashCodes.NotEnoughMoney;
            }
            else
            {
                return CashCodes.AcceptableAmount;
            }
        }
    }
}

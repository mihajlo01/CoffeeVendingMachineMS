using CoffeeVendingMachineMS.BusinessLogic;
using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CoffeeVendingMachineMS
{
    public class CoffeeMachineController
    {
        private readonly ICoffeeTypeBusinessLogic coffeeTypeBusinessLogic;
        private readonly IMoneyBusinessLogic moneyBusinessLogic;
        static int pickedCoffeeCode = 0;
        CashCodes code;

        public CoffeeMachineController()
        {
            coffeeTypeBusinessLogic = new CoffeeTypeBusinessLogic();
            moneyBusinessLogic = new MoneyBusinessLogic();
        }

        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var coffeeTypes = coffeeTypeBusinessLogic.GetCoffeeTypes().Result;
            long coffeeTypesCount = coffeeTypeBusinessLogic.GetCoffeeTypesCount();
            List<int> coffeeTypeCodes = new List<int>();

            HelloMessage();

            Console.WriteLine("This machine is accepting only € coins!");
            Console.WriteLine("Enter a character in order to stop entering coins.\n");
            Console.Write("Please insert coins:");

            while (code != CashCodes.WrongInput)
            {
                InsertCash();
            }

            Console.Clear();

            HelloMessage();
            Console.WriteLine("Your balance is: " + moneyBusinessLogic.Ballance + "€\n");
            Console.WriteLine("Please choose from the " + coffeeTypesCount + " types of coffee that I offer:");
            
            foreach(var coffeeType in coffeeTypes)
            {
                coffeeTypeCodes.Add(coffeeType.Code);
                Console.WriteLine(coffeeType.Code + ". " + coffeeType.Name + " - " + coffeeType.Price + "€");
            }

            while (!coffeeTypeCodes.Contains(pickedCoffeeCode))
            {
                UsersChoice();
            }

            code = moneyBusinessLogic.CheckOrderPrice(coffeeTypes.Where(x => x.Code == pickedCoffeeCode).Select(x => x.Price).First());

            if(code == CashCodes.AcceptableAmount)
            {
                Console.WriteLine("Great! Now let's spice it up!");
                Console.ReadLine();
            }
        }

        private void UsersChoice()
        {
            Console.Write("\nEnter your choice: ");
            var pickedCoffeeCodeS = Console.ReadLine();

            if(!Int32.TryParse(pickedCoffeeCodeS, out pickedCoffeeCode))
            {
                Console.WriteLine("Please make sure it's a number from the offered coffees!");
            }

            return;
        }

        public void InsertCash()
        {
            code = moneyBusinessLogic.CheckAndUpdateBalance(Console.ReadLine());

            if(code == CashCodes.BelowMinimum)
            {
                Console.WriteLine("Not enough money inserted!");
                Console.Write("Please insert coins:");
            }
            if(code == CashCodes.NotValid)
            {
                Console.WriteLine("Your coin is not valid! Please search which coins are valid for the € currency!");
                Console.Write("Please insert coins:");
            }
        }

        public void HelloMessage()
        {
            Console.WriteLine("Hi there, I'm your virtual coffee vending machine!\n");
        }
    }
}

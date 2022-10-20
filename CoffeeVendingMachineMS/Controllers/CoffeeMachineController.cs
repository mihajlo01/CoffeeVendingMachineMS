using CoffeeVendingMachineMS.BusinessLogic;
using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;

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

            Console.WriteLine("Hi there, I'm your virtual coffee vending machine!\n");

            Console.WriteLine("This machine is accepting only 1€ coins!");
            Console.WriteLine("Enter a character in order to stop entering coins.\n");
            Console.Write("Please insert coins:");

            while (code != CashCodes.WrongInput)
            {
                InsertCash();
            }

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
                Console.WriteLine("Not enough money inserted! Returning coins...");
            }
        }
    }
}

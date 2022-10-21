using CoffeeVendingMachineMS.BusinessLogic;
using CoffeeVendingMachineMS.Controllers;
using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeVendingMachineMS
{
    public class CoffeeMachineController
    {
        private readonly ICoffeeTypeBusinessLogic coffeeTypeBusinessLogic;
        private readonly IMoneyBusinessLogic moneyBusinessLogic;
        private AddonsController addonsController;

        public Order order = new Order();
        private CoffeeType pickedCoffeeType = new CoffeeType();
        static int pickedCoffeeCode = 0;
        CashCodes code;

        public CoffeeMachineController()
        {
            coffeeTypeBusinessLogic = new CoffeeTypeBusinessLogic();
            moneyBusinessLogic = new MoneyBusinessLogic();
            addonsController = new AddonsController(moneyBusinessLogic);
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

            while (code != CashCodes.WrongInput)
            {
                InsertCash();
            }

            Console.Clear();

            HelloMessage();
            Console.WriteLine("Your balance is: " + moneyBusinessLogic.Balance + "€\n");
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

            pickedCoffeeType = coffeeTypes.Where(x => x.Code == pickedCoffeeCode).FirstOrDefault();
            code = moneyBusinessLogic.UpdateOrderPrice(coffeeTypes.Where(x => x.Code == pickedCoffeeCode).Select(x => x.Price).First());

            if(code == CashCodes.AcceptableAmount)
            {
                Console.Clear();
                Console.WriteLine("Good choice! Now, let's spice it up!\n");
                Console.WriteLine("Your "+ pickedCoffeeType.Name +" contains: Sugar: " + pickedCoffeeType.Sugar + ", MilkDose: " + pickedCoffeeType.MilkDose +
                    ", Contains Cream: " + pickedCoffeeType.Cream + ", Contains Caramelle: " + pickedCoffeeType.Caramelle);
                addonsController.AskForAddons();
                Console.ReadLine();
            }
            else
            {
                
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
            Console.Write("Insert coins: ");
            code = moneyBusinessLogic.CheckAndUpdateBalance(Console.ReadLine());

            if(code == CashCodes.BelowMinimum)
            {
                Console.WriteLine("Not enough money inserted!");
            }
            if(code == CashCodes.NotValid)
            {
                Console.WriteLine("Your coin is not valid! Please search which coins are valid for the € currency!");
            }
        }

        public void HelloMessage()
        {
            Console.WriteLine("Hi there, I'm your virtual coffee vending machine!\n");
        }
    }
}

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
        private readonly ICoffeeTypeRepository coffeeTypeRepository;
        private readonly IMoneyRepository moneyRepository;
        private AddonsController addonsController;

        private CoffeeType pickedCoffeeType = new CoffeeType();
        static int pickedCoffeeCode = 0;
        CashCodes code;

        public CoffeeMachineController()
        {
            coffeeTypeRepository = new CoffeeTypeRepository();
            moneyRepository = new MoneyRepository();
            addonsController = new AddonsController(moneyRepository);
        }

        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var coffeeTypes = coffeeTypeRepository.GetCoffeeTypes().Result;
            long coffeeTypesCount = coffeeTypeRepository.GetCoffeeTypesCount();
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
            Console.WriteLine("Your balance is: " + moneyRepository.Balance + "€\n");
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
            code = moneyRepository.UpdateOrderPrice(coffeeTypes.Where(x => x.Code == pickedCoffeeCode).Select(x => x.Price).First());

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
            code = moneyRepository.CheckAndUpdateBalance(Console.ReadLine());

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

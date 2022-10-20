using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;

namespace CoffeeVendingMachineMS
{
    public class CoffeeMachineController
    {
        private readonly ICoffeeTypeRepository coffeeTypeRepository;
        static int pickedCoffeeCode = 0;

        public CoffeeMachineController()
        {
            coffeeTypeRepository = new CoffeeTypeBusinessLogic();
        }

        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var coffeeTypes = coffeeTypeRepository.GetCoffeeTypes().Result;
            long coffeeTypesCount = coffeeTypeRepository.GetCoffeeTypesCount();
            List<int> coffeeTypeCodes = new List<int>();

            Console.WriteLine("Hi there, I'm your virtual coffee vending machine!");
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
            var pickedCoffeeType = Console.ReadLine();

            if(!Int32.TryParse(pickedCoffeeType, out pickedCoffeeCode))
            {
                Console.WriteLine("Please make sure it's a number from the offered coffees!");
            }

            return;
        }
    }
}

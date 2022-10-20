using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using System;
using System.Collections.Generic;

namespace CoffeeVendingMachineMS
{
    public class CoffeeMachine
    {
        private readonly ICoffeeTypeRepository coffeeTypeRepository;

        public CoffeeMachine()
        {
            coffeeTypeRepository = new CoffeeTypeRepository();
        }

        public void Start()
        {
            var coffeeTypes = coffeeTypeRepository.GetCoffeeTypes().Result;
            long coffeeTypesCount = coffeeTypeRepository.GetCoffeeTypesCount();
            List<int> coffeeTypeCodes = new List<int>();

            Console.WriteLine("Hi there, I'm your virtual coffee vending machine!");
            Console.WriteLine("Please choose from the " + coffeeTypesCount + " types of coffee that I offer:");
            
            foreach(var coffeeType in coffeeTypes)
            {
                coffeeTypeCodes.Add(coffeeType.Code);
                Console.WriteLine(coffeeType.Code + ". " + coffeeType.Name);
            }

            var pickedCoffeeType = UsersChoice();
        }

        private int UsersChoice()
        {
            Console.WriteLine("Enter your choice:");
            var pickedCoffeeType = Console.ReadLine();
            int pickedCoffeeNumber;

            if(!Int32.TryParse(pickedCoffeeType, out pickedCoffeeNumber))
            {
                Console.WriteLine("Please make sure it's a number from the offered coffees!");
                UsersChoice();
            }

            Console.WriteLine("Excelent!");
            return pickedCoffeeNumber;
        }
    }
}

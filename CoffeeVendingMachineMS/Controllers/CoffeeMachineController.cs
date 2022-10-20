using CoffeeVendingMachineMS.BusinessLogic;
using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CoffeeVendingMachineMS
{
    public class CoffeeMachineController
    {
        private readonly ICoffeeTypeBusinessLogic coffeeTypeBusinessLogic;
        private readonly IMoneyBusinessLogic moneyBusinessLogic;
        public Order order = new Order();
        private CoffeeType pickedCoffeeType = new CoffeeType();
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

            pickedCoffeeType = coffeeTypes.Where(x => x.Code == pickedCoffeeCode).FirstOrDefault();
            code = moneyBusinessLogic.UpdateInitialOrderPrice(coffeeTypes.Where(x => x.Code == pickedCoffeeCode).Select(x => x.Price).First());

            if(code == CashCodes.AcceptableAmount)
            {
                Console.Clear();
                Console.WriteLine("Good choice! Now let's spice it up!\n");
                Console.WriteLine("Your "+ pickedCoffeeType.Name +"contains: Sugar: " + pickedCoffeeType.Sugar + ", MilkDose: " + pickedCoffeeType.MilkDose +
                    ", Contains Cream: " + pickedCoffeeType.Cream + ", Contains Caramelle: " + pickedCoffeeType.Caramelle);
                ShowBallance();
                AskForAddons();
                Console.ReadLine();
            }
            else
            {
                System.Environment.Exit(1);
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

        public void AskForAddons()
        {
            var addons = LoadAddons();
            Console.WriteLine("Would you like to add anything extra?");
            Console.WriteLine( "0. None");
            Console.WriteLine(addons.Sugar.Code + ". " + addons.Sugar.Name + " - " + addons.Sugar.Price + "€");
            Console.WriteLine(addons.Milk.Code + ". " + addons.Milk.Name + " - " + addons.Milk.Price + "€");
            Console.WriteLine(addons.Cream.Code + ". " + addons.Cream.Name + " - " + addons.Cream.Price + "€");
            Console.WriteLine(addons.Caramelle.Code + ". " + addons.Caramelle.Name + " - " + addons.Caramelle.Price + "€");
            Console.Write("Enter the prefered number:");
            Console.ReadLine();
        }

        public void HelloMessage()
        {
            Console.WriteLine("Hi there, I'm your virtual coffee vending machine!\n");
        }
        
        public void ShowBallance()
        {
            Console.WriteLine("Your balance is: " + moneyBusinessLogic.Ballance + "€\n");
        }

        public static Addons LoadAddons()
        {
            using (StreamReader r = new StreamReader("C:/Users/Mihajlo/source/repos/CoffeeVendingMachineMS/CoffeeVendingMachineMS/addons.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Addons>(json);
            }
        }
    }
}

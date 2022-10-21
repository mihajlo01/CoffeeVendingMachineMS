using CoffeeVendingMachineMS.BusinessLogic;
using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace CoffeeVendingMachineMS.Controllers
{
    public class AddonsController
    {
        private readonly IMoneyRepository moneyRepository;

        public AddonsController(IMoneyRepository moneyRepository)
        {
            this.moneyRepository = moneyRepository;
        }

        public void AskForAddons()
        {
            ShowBallance();
            var addons = LoadAddons();
            Console.WriteLine("Would you like to add anything extra?");
            Console.WriteLine("Enter 0 to finish up topping your coffee :)\n");
            Console.WriteLine("Choose your addon:");
            Console.WriteLine("0. None");
            foreach (var addon in addons)
            {
                Console.WriteLine(addon.Code + ". " + addon.Name + " - " + addon.Price + "€");
            }

            Console.Write("Enter the prefered number: ");

            while (ProcessAddon(Console.ReadLine()) != Convert.ToInt32(AddonTypes.None))
            {
                Console.Clear();
                AskForAddons();
            }

            ProcessOrder();
        }

        public int ProcessAddon(string addon)
        {
            int addonCode = 0;
            var addons = LoadAddons();

            if (!Int32.TryParse(addon, out addonCode) || addonCode < 0 || (!addons.Select(x => x.Code).ToList().Contains(addonCode) && addon != "0"))
            {
                Console.Write("Please enter a valid number: ");
                ProcessAddon(Console.ReadLine());
                return 1;
            }
            if(addonCode == 0)
            {
                return Convert.ToInt32(AddonTypes.None);
            }
            else
            {
                var addonPrice = addons.Where(x => x.Code == addonCode).Select(x => x.Price).First();
                if(moneyRepository.UpdateOrderPrice(addonPrice) == CashCodes.NotEnoughMoney)
                {
                    Console.WriteLine("Not enough money for the selected option!\n");
                    Console.Write("Please enter again: ");
                    ProcessAddon(Console.ReadLine());
                }
                return 1;
            }
        }

        public void ProcessOrder()
        {
            Console.WriteLine("\nThank you for using this virtual coffee vending machine!\n");
            Console.WriteLine("Yor order total is: " + moneyRepository.OrderTotal + "€");
            if (moneyRepository.Balance > 0)
            {
                Console.WriteLine("Yor change is: " + moneyRepository.Balance + "€");
            }

            Console.WriteLine("Please wait for the order to finish...\n");
            Thread.Sleep(6000);

            Console.WriteLine("Enjoy your coffee!");
            Thread.Sleep(7000);

            System.Environment.Exit(1);
        }

        public void ShowBallance()
        {
            Console.WriteLine("Your balance is: " + moneyRepository.Balance + "€\n");
        }

        public static List<Addon> LoadAddons()
        {
            using (StreamReader r = new StreamReader("C:/Users/Mihajlo/source/repos/CoffeeVendingMachineMS/CoffeeVendingMachineMS/addons.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Addon>>(json);
            }
        }
    }
}

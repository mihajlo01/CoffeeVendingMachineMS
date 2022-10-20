using System;
using System.Collections.Generic;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using CoffeeVendingMachineMS.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CoffeeVendingMachineMS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachine();
            coffeeMachine.Start();
        }
    }
}
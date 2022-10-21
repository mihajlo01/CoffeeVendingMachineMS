using CoffeeVendingMachineMS.Enums;
using CoffeeVendingMachineMS.Interfaces;
using CoffeeVendingMachineMS.Models;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeVendingMachineMS.Test
{
    public class CoffeeVendingMachineTest
    {
        Mock<IBaseRepository> baseRepository;
        Mock<ICoffeeTypeRepository> coffeeTypeRepository;
        Mock<IMoneyRepository> moneyRepository;

        [SetUp]
        public async Task Setup()
        {
            baseRepository = new Mock<IBaseRepository>();
            coffeeTypeRepository = new Mock<ICoffeeTypeRepository>();
            moneyRepository = new Mock<IMoneyRepository>();
        }

        [Test]
        public async Task Database()
        {
            baseRepository.Setup(b => b.Database()).Returns(It.IsAny<IMongoDatabase>());
        }

        [Test]
        public async Task CoffeeTypeRepository()
        {
            long wantedResult = 5;

            coffeeTypeRepository.Setup(ct => ct.GetCoffeeTypes()).Returns(It.IsAny<Task<IEnumerable<CoffeeType>>>());
            coffeeTypeRepository.Setup(ct => ct.GetCoffeeTypesCount()).Returns(wantedResult);
        }

        [Test]
        public async Task MoneyRepository()
        {
            long wantedResult = 5;

            moneyRepository.Setup(m => m.CheckAndUpdateBalance("2")).Returns(CashCodes.AcceptableAmount);
            moneyRepository.Setup(m => m.CheckAndUpdateBalance("g")).Returns(CashCodes.NotValid);
            moneyRepository.Setup(m => m.CheckAndUpdateBalance("0.25")).Returns(CashCodes.BelowMinimum);

            moneyRepository.Setup(m => m.UpdateOrderPrice(1)).Returns(CashCodes.NotEnoughMoney);
            moneyRepository.Setup(m => m.UpdateOrderPrice(-1)).Returns(CashCodes.AcceptableAmount);
        }
    }
}

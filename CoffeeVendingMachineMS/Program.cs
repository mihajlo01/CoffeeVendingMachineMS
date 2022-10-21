namespace CoffeeVendingMachineMS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var coffeeMachine = new CoffeeMachineController();
            coffeeMachine.Start();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse;

namespace PumpkinApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create market instance
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());

            //Process user orders
            market.BuyPumpkin(new Account("Client A"), 10);
            market.BuyPumpkin(new Account("Client B"), 11);
            market.SellPumpkin(new Account("Client C"), 15);
            market.SellPumpkin(new Account("Client D"), 9);
            market.BuyPumpkin(new Account("Client E"), 10);
            market.SellPumpkin(new Account("Client F"), 10);
            market.BuyPumpkin(new Account("Client G"), 100);

            Console.ReadLine();
        }
    }
}

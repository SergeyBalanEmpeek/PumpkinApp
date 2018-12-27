using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Warehouse;
namespace WarehouseTest
{
    [TestClass]
    public class UnitTest1
    {
        
        #region Zero Users Tests
        [TestMethod]
        public void OrderZeroUserTest1()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            List<Order> orderList = market.Orders;

            //By default no orders are expected
            Assert.AreEqual(0, orderList.Count);         
        }
        #endregion
        
        #region One User Tests
        [TestMethod]
        public void OrderOneUserTest1()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.BuyPumpkin(new Account("Client A"), 10);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(1, orderList.Count);
        }
        #endregion

        #region Two Users Test
        [TestMethod]
        public void OrdersTwoUsersTest1()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.BuyPumpkin(new Account("Client A"), 10);
            market.BuyPumpkin(new Account("Client B"), 10);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(2, orderList.Count);
        }

        [TestMethod]
        public void OrdersTwoUsersTest2()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.BuyPumpkin(new Account("Client A"), 10);
            market.SellPumpkin(new Account("Client B"), 10);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(0, orderList.Count);
        }

        [TestMethod]
        public void OrdersTwoUsersTest3()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.SellPumpkin(new Account("Client A"), 10);
            market.BuyPumpkin(new Account("Client B"), 10);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(0, orderList.Count);
        }

        [TestMethod]
        public void OrdersTwoUsersTest4()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.SellPumpkin(new Account("Client A"), 10);
            market.BuyPumpkin(new Account("Client B"), 9);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(2, orderList.Count);
        }

        [TestMethod]
        public void OrdersTwoUsersTest5()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.SellPumpkin(new Account("Client A"), 9);
            market.BuyPumpkin(new Account("Client B"), 10);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(0, orderList.Count);
        }
        #endregion

        #region Three Users Test
        [TestMethod]
        public void OrdersThreeUsersTest1()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.BuyPumpkin(new Account("Client A"), 10);
            market.BuyPumpkin(new Account("Client B"), 10);
            market.SellPumpkin(new Account("Client C"), 1);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(1, orderList.Count);
            Assert.AreEqual("Client B", orderList.FirstOrDefault().Account.Name);           //Earliest price chosen
        }

        [TestMethod]
        public void OrdersThreeUsersTest2()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.BuyPumpkin(new Account("Client A"), 11);
            market.BuyPumpkin(new Account("Client B"), 10);
            market.SellPumpkin(new Account("Client C"), 1);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(1, orderList.Count);
            Assert.AreEqual("Client B", orderList.FirstOrDefault().Account.Name);           //Sell - highest price chosen
        }

        [TestMethod]
        public void OrdersThreeUsersTest3()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());
            market.SellPumpkin(new Account("Client A"), 11);
            market.SellPumpkin(new Account("Client B"), 10);
            market.BuyPumpkin(new Account("Client C"), 13);

            List<Order> orderList = market.Orders;

            Assert.AreEqual(1, orderList.Count);
            Assert.AreEqual("Client A", orderList.FirstOrDefault().Account.Name);           //Buy - lowest price chosen
        }
        #endregion

        #region Multi-threading
        [TestMethod]
        public void MultiTreadTest1()
        {
            Market market = new Market(new ListOrderRepository(), new ConsoleOutput());


            Thread thread1 = new Thread(new ThreadStart(delegate () {
                for(int i = 0; i < 500; i++)
                {
                    market.SellPumpkin(new Account($"Account {i}"), 10);
                    market.BuyPumpkin(new Account($"Account {i}"), 10);
                }
            }));
            
            Thread thread2 = new Thread(new ThreadStart(delegate () {
                for (int i = 0; i < 500; i++)
                {
                    market.SellPumpkin(new Account($"Account {i}"), 10);
                    market.BuyPumpkin(new Account($"Account {i}"), 10);
                }
            })); 

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            List<Order> orderList = market.Orders;

            Assert.AreEqual(0, orderList.Count);
        }
        #endregion
        
    }
}

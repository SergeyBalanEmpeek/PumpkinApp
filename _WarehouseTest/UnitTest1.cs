using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse;

namespace WarehouseTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MainStorage storage = new MainStorage();

            storage.BuyPumpkin(new Account("Client A"), 10);
            storage.BuyPumpkin(new Account("Client B"), 11);
            storage.SellPumpkin(new Account("Client C"), 15);

            PrivateObject obj = new PrivateObject(target);
            var retVal = obj.Invoke("PrivateMethod");

        }
    }
}

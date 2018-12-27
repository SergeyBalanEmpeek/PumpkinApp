using System.Collections.Generic;
using System.Linq;

namespace Warehouse
{
    /// <summary>
    /// A market, where we can store and sell products
    /// </summary>
    public class Market
    {
        private IOrderRepository ordersRepository;
        private IResultsOutput output;

        /// <summary>
        /// Market constructor
        /// </summary>
        /// <param name="ordersRepository">Orders Repository</param>
        /// <param name="output">A way to output results</param>
        public Market(IOrderRepository ordersRepository, IResultsOutput output)
        {
            this.ordersRepository = ordersRepository;
            this.output = output;
        }

        #region Generic Add/Sell methods
        /// <summary>
        /// Buy an item
        /// </summary>
        /// <param name="account">User who want to make a purchase</param>
        /// <param name="product">Product name</param>
        /// <param name="price">Desired price to purchase an item</param>
        public void Buy(Account account, Product product, decimal price)
        {
            RegisterOrder(account, product, OperationType.Buy, price);
        }

        /// <summary>
        /// Sell an item
        /// </summary>
        /// <param name="account">User who want to make a sell</param>
        /// <param name="product">Product name</param>
        /// <param name="price">Desired price to sell an item</param>
        public void Sell(Account account, Product product, decimal price)
        {
            RegisterOrder(account, product, OperationType.Sell, price);
        }
        #endregion

        #region Pumpkin related methods
        /// <summary>
        /// Buy a Pumpkin
        /// </summary>
        /// <param name="account">User who want to make a purchase</param>
        /// <param name="price">Desired price to purchase an item</param>
        public void BuyPumpkin(Account account, decimal price)
        {
            RegisterOrder(account, new Pumpkin(), OperationType.Buy, price);
        }

        /// <summary>
        /// Sell a Pumpkin
        /// </summary>
        /// <param name="account">User who want to make a sell</param>
        /// <param name="price">Desired price to sell an item</param>
        public void SellPumpkin(Account account,  decimal price)
        {
            RegisterOrder(account, new Pumpkin(), OperationType.Sell, price);
        }
        #endregion

        /// <summary>
        /// Get existing orders at market
        /// </summary>
        public List<Order> Orders
        {
            get
            {
                return ordersRepository.GetOrders();
            }
        }

        /// <summary>
        /// Register order
        /// </summary>
        /// <param name="account">User who want to make a sell</param>
        /// <param name="product">Product name</param>
        /// <param name="type">Operation type</param>
        /// <param name="price">Desired price to sell an item</param>
        private void RegisterOrder(Account account, Product product, OperationType type, decimal price)
        {
            Order newOrder = new Order(account, product, type, price);

            try
            {
                ordersRepository.BeginSafeOperation();      //Start data locking

                IEnumerable<Order> filteredOrders = ordersRepository.GetOrders().Where(c => c.Type != newOrder.Type);             //Remove orders with same type

                if (newOrder.Type == OperationType.Buy)
                {
                    //Purchasing operation

                    //Lowest price should be first
                    filteredOrders = filteredOrders.OrderBy(c => c.Price).ThenBy(c => c.Date);

                    //Find any purchasable order 
                    Order deal = filteredOrders.Where(c => newOrder.Price >= c.Price).FirstOrDefault();
                    if (deal != null)
                    {
                        //Console.WriteLine($"{newOrder.Account.Name} bought {product.Name} from {deal.Account.Name} for {newOrder.Price}$");
                        output.SaleEvent(newOrder.Account, deal.Account, product, newOrder.Price, OperationType.Buy);

                        //Remove this deal from order list since it's completed now
                        ordersRepository.RemoveOrder(deal);
                        return;
                    }
                }
                else
                {
                    //Selling operation

                    //Highest price should be first
                    filteredOrders = filteredOrders.OrderByDescending(c => c.Price).ThenBy(c => c.Date);

                    //Find any purchasable order 
                    Order deal = filteredOrders.Where(c => c.Price >= newOrder.Price).FirstOrDefault();
                    if (deal != null)
                    {
                        //Console.WriteLine($"{newOrder.Account.Name} sold {product.Name} to {deal.Account.Name} for {newOrder.Price}$");
                        output.SaleEvent(deal.Account, newOrder.Account, product, newOrder.Price, OperationType.Sell);

                        //Remove this deal from order list since it's completed now
                        ordersRepository.RemoveOrder(deal);
                        return;
                    }
                }

                //if we are here, we were unable to satisfy this order. Put it to query
                ordersRepository.AddOrder(newOrder);
            }

            finally
            {
                ordersRepository.FinishSafeOperation();      //Release data locking
            }
        }
    }
}

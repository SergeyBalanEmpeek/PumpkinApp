using System.Collections.Generic;
using System.Threading;

namespace Warehouse
{
    
    /// <summary>
    /// List Order Repository
    /// </summary>
    public class ListOrderRepository: IOrderRepository
    {
        /// <summary>
        /// List of all orders
        /// </summary>
        private List<Order> orders { get; } = new List<Order>();

        /// <summary>
        /// Get existing orders
        /// </summary>
        public List<Order> GetOrders()
        {
            return orders;
        }

        /// <summary>
        /// Store order
        /// </summary>
        /// <param name="order">Order to store</param>
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        /// <summary>
        /// Remove order
        /// </summary>
        /// <param name="order">Order to remove</param>
        public void RemoveOrder(Order order)
        {
            orders.Remove(order);
        }

        /// <summary>
        /// Begin safe operation with data
        /// </summary>
        public void BeginSafeOperation()
        {
            Monitor.Enter(orders);
        }

        /// <summary>
        /// Finish safe operation with data
        /// </summary>
        public void FinishSafeOperation()
        {
            Monitor.Exit(orders);
        }
    }
    
}

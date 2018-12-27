using System.Collections.Generic;

namespace Warehouse
{
    /// <summary>
    /// Interface for all order repositories
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Get existing orders
        /// </summary>
        List<Order> GetOrders();

        /// <summary>
        /// Store order
        /// </summary>
        /// <param name="order">Order to store</param>
        void AddOrder(Order order);

        /// <summary>
        /// Remove order
        /// </summary>
        /// <param name="order">Order to remove</param>
        void RemoveOrder(Order order);

        /// <summary>
        /// Begin safe operation with data
        /// </summary>
        void BeginSafeOperation();

        /// <summary>
        /// Finish safe operation with data
        /// </summary>
        void FinishSafeOperation();
    }
}

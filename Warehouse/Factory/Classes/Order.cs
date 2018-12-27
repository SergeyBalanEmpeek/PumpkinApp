using System;

namespace Warehouse
{
    /// <summary>
    /// Order, made by account
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Account, who made an order
        /// </summary>
        public Account Account { get; private set; }

        /// <summary>
        /// Current product
        /// </summary>
        public Product Product { get; private set; }

        /// <summary>
        /// Action type
        /// </summary>
        public OperationType Type { get; private set; }

        /// <summary>
        /// Desired price of deal
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// Action register date
        /// </summary>
        public DateTime Date { get; private set; }


        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="account">Account, who made an order</param>
        /// <param name="product">Related product</param>
        /// <param name="type">Operation type</param>
        /// <param name="price">Desired price</param>
        public Order(Account account, Product product, OperationType type, decimal price)
        {
            Account = account;
            Product = product;
            Type = type;
            Price = price;
            Date = DateTime.UtcNow;
        }

    }
}

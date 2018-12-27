using System;

namespace Warehouse
{
    /// <summary>
    /// Output results to console application
    /// </summary>
    public class ConsoleOutput : IResultsOutput
    {
        /// <summary>
        /// Process action results
        /// </summary>
        /// <param name="purchaser">User who purchased a product</param>
        /// <param name="seller">User who sold a product</param>
        /// <param name="product">Product that was sold</param>
        /// <param name="price">Product cost</param>
        /// <param name="type">Who was initiator of action. You may customize result according to initiator's action</param>
        public void SaleEvent(Account purchaser, Account seller, Product product, decimal price, OperationType type = OperationType.Buy)
        {
            switch(type)
            {
                default:
                case OperationType.Buy:
                    Console.WriteLine($"{purchaser.Name} bought {product.Name} from {seller.Name} for {price}$");
                    break;

                case OperationType.Sell:
                    Console.WriteLine($"{seller.Name} sold {product.Name} to {purchaser.Name} for {price}$");
                    break;
            }
        }
    }
}

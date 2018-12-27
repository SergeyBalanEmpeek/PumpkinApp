using System.Collections.Generic;

namespace Warehouse
{
    /// <summary>
    /// Interface for all result output types
    /// </summary>
    public interface IResultsOutput
    {
        /// <summary>
        /// Process action results
        /// </summary>
        /// <param name="purchaser">User who purchased a product</param>
        /// <param name="seller">User who sold a product</param>
        /// <param name="product">Product that was sold</param>
        /// <param name="price">Product cost</param>
        /// <param name="type">Who was initiator of this action. Optional, you may customize result according to initiator's action</param>
        void SaleEvent(Account purchaser, Account seller, Product product, decimal price, OperationType type = OperationType.Buy);
    }
}

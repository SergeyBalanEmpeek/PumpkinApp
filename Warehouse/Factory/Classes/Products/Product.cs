using System;

namespace Warehouse
{
    /// <summary>
    /// Product that could be sold
    /// </summary>
    public abstract class Product
    {
        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; }

        public Product(string name)
        {
            Name = name;
        }
    }
}

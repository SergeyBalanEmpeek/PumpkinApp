using System;

namespace Warehouse
{
    /// <summary>
    /// User, who wish purchase or sell product
    /// </summary>
    public sealed class Account
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Create new account
        /// </summary>
        /// <param name="Name">account name</param>
        public Account(string Name)
        {
            this.Name = Name;
        }
    }
}

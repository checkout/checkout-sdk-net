using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class TabbyOptions
    {
        /// <summary>
        /// Tabby installments payment option configuration
        /// </summary>
        public TabbyInstallments Installments { get; set; }
    }

    public class TabbyInstallments
    {
        /// <summary>
        /// The unique identifier for the Tabby installments option
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the Tabby installments option
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Tabby installments option
        /// </summary>
        public IList<string> Flags { get; set; }
    }
}
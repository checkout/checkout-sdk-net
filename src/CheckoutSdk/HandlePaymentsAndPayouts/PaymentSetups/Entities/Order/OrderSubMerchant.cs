using System;

namespace Checkout.Payments.Setups.Entities
{
    public class OrderSubMerchant
    {
        /// <summary>
        /// The unique identifier of the sub-merchant
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The sub-merchant's product category
        /// </summary>
        public string ProductCategory { get; set; }

        /// <summary>
        /// The number of orders the sub-merchant has processed
        /// </summary>
        public int? NumberOfTrades { get; set; }

        /// <summary>
        /// The sub-merchant's registration date
        /// </summary>
        public DateTime? RegistrationDate { get; set; }
    }
}
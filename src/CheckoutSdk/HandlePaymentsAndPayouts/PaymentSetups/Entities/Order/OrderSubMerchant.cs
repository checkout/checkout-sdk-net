namespace Checkout.Payments.Setups.Entities
{
    public class OrderSubMerchant
    {
        /// <summary>
        /// The unique identifier of the sub-merchant
        /// [Optional]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The sub-merchant's product category
        /// [Optional]
        /// </summary>
        public string ProductCategory { get; set; }

        /// <summary>
        /// The number of orders the sub-merchant has processed.
        /// [Optional]
        /// </summary>
        public int? NumberOfSales { get; set; }

        /// <summary>
        /// The registration date of the sub-merchant (yyyy-MM-dd).
        /// [Optional]
        /// Format: date
        /// </summary>
        public string RegistrationDate { get; set; }
    }
}
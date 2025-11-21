namespace Checkout.Payments.Setups.Entities
{
    public class OrderSubMerchant
    {
        /// <summary>
        /// The unique identifier of the sub-merchant
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The category of products or services offered by the sub-merchant
        /// </summary>
        public string ProductCategory { get; set; }

        /// <summary>
        /// The number of trades or transactions the sub-merchant has conducted
        /// </summary>
        public int? NumberOfTrades { get; set; }

        /// <summary>
        /// The date when the sub-merchant was registered in YYYY-MM-DD format
        /// </summary>
        public string RegistrationDate { get; set; }
    }
}
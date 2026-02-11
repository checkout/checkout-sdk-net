namespace Checkout.PaymentMethods.Entities
{
    public class PaymentMethod
    {
        /// <summary>
        /// The type of the payment method
        /// [Required]
        /// </summary>
        public PaymentMethodType? Type { get; set; }

        /// <summary>
        /// Name of the payment method
        /// </summary>
        public string Name { get; set; } 

        /// <summary>
        /// The unique identifier for the merchant, provided by the partner
        /// </summary>
        public string PartnerMerchantId { get; set; }
    }
}
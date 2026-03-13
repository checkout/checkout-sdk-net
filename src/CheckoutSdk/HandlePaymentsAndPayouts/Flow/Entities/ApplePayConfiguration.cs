namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class ApplePayConfiguration : PaymentMethodConfigurationBase
    {
        /// <summary>
        /// The type of the Apple Pay payment total line item. Default: "final"
        /// </summary>
        public TotalType? TotalType { get; set; } = Entities.TotalType.Final;
    }
}
namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class GooglePayConfiguration : PaymentMethodConfigurationBase
    {
        /// <summary>
        /// The status of the Google Pay payment total price. Default: "final"
        /// </summary>
        public TotalPriceStatus? TotalPriceStatus { get; set; } = Entities.TotalPriceStatus.Final;
    }
}
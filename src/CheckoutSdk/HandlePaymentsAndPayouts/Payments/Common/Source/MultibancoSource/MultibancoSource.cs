namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    MultibancoSource
{
    /// <summary>
    /// multibanco source Class
    /// The source of the payment
    /// </summary>
    public class MultibancoSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the MultibancoSource class.
        /// </summary>
        public MultibancoSource() : base(SourceType.Multibanco)
        {
        }

        /// <summary>
        /// Multibanco payment reference
        /// [Optional]
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// The identifier of a supplier charging for its service or product
        /// [Optional]
        /// </summary>
        public string ServiceSupplierId { get; set; }
    }
}
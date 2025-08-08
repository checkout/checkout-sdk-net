namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    BancontactSource
{
    /// <summary>
    /// bancontact source Class
    /// The source of the payment
    /// </summary>
    public class BancontactSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the BancontactSource class.
        /// </summary>
        public BancontactSource() : base(SourceType.Bancontact)
        {
        }

        /// <summary>
        /// The IBAN of the Consumer Bank account used for payment (if applicable).
        /// [Optional]
        /// <= 34
        /// </summary>
        public string Iban { get; set; }
    }
}
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// Bacs Direct Debit source.
    /// </summary>
    public class RequestBacsSource : AbstractRequestSource
    {
        public RequestBacsSource() : base(PaymentSourceType.Bacs)
        {
        }

        /// <summary>
        /// The Bacs Direct Debit instrument ID.
        /// [Required]
        /// Pattern: ^(src)_(\w{26})$
        /// </summary>
        public string Id { get; set; }
    }
}

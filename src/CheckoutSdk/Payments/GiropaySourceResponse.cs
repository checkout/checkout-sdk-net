using Checkout.Common;

namespace Checkout.Payments
{
    /// <summary>
    /// The card used to complete a payment request. 
    /// </summary>
    public class GiropaySourceResponse : IResponseSource
    {        
        /// <summary>
        /// Gets or sets the type of source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the purpose of source.
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// Gets or sets the bic of source.
        /// </summary>
        public string Bic { get; set; }

    }
}
using Checkout.StandaloneAccountUpdater.Entities;

namespace Checkout.StandaloneAccountUpdater.Requests
{
    public class CardDetails : CardBase
    {
        /// <summary>
        /// The card number
        /// </summary>
        /// [Required]
        public string Number { get; set; }
    }
}
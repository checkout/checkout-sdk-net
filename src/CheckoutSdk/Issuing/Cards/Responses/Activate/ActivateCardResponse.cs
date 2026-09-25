using Checkout.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Activate
{
    /// <summary>
    /// The response to a card activation request.
    /// </summary>
    public class ActivateCardResponse : Resource
    {
        /// <summary>
        /// The date and time the card was activated.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? LastActivatedOn { get; set; }
    }
}

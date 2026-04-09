using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.HandlePaymentsAndPayouts.GooglePay.Responses
{
    /// <summary>
    /// Response containing the list of registered web domains for a Google Pay enrolled entity.
    /// Required (API): domains.
    /// </summary>
    public class GooglePayDomainListResponse : Resource
    {
        /// <summary>
        /// The list of domains registered for the entity.
        /// [Required]
        /// Items format: hostname
        /// </summary>
        public IList<string> Domains { get; set; }
    }
}

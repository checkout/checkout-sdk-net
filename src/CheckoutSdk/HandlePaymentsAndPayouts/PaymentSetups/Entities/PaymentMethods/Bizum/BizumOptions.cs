using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class BizumOptions
    {
        /// <summary>
        /// Bizum immediate payment option configuration
        /// </summary>
        public BizumPayNow PayNow { get; set; }
    }

    public class BizumPayNow
    {
        /// <summary>
        /// The unique identifier for the Bizum immediate payment option
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the Bizum immediate payment option
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Bizum immediate payment option
        /// </summary>
        public IList<string> Flags { get; set; }
    }
}
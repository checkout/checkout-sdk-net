using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class StcpayOptions
    {
        /// <summary>
        /// STC Pay full payment option configuration
        /// </summary>
        public StcpayPayInFull PayInFull { get; set; }
    }

    public class StcpayPayInFull
    {
        /// <summary>
        /// The unique identifier for the STC Pay full payment option
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the STC Pay full payment option
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the STC Pay full payment option
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The action configuration for STC Pay full payment
        /// </summary>
        public StcpayAction Action { get; set; }
    }

    public class StcpayAction
    {
        /// <summary>
        /// The type of action to be performed with STC Pay
        /// </summary>
        public string Type { get; set; }
    }
}
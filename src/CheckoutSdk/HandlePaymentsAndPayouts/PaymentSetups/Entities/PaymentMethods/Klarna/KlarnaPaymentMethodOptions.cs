using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class KlarnaPaymentMethodOptions
    {
        /// <summary>
        /// Klarna SDK configuration options
        /// </summary>
        public KlarnaOptionsSdk Sdk { get; set; }
    }

    public class KlarnaOptionsSdk
    {
        /// <summary>
        /// The unique identifier for the Klarna SDK option
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The status of the Klarna SDK option
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Configuration flags for the Klarna SDK
        /// </summary>
        public IList<string> Flags { get; set; }

        /// <summary>
        /// The action configuration for Klarna SDK
        /// </summary>
        public KlarnaAction Action { get; set; }
    }

    public class KlarnaAction
    {
        /// <summary>
        /// The type of action to be performed with Klarna
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The client token for Klarna authentication
        /// </summary>
        public string ClientToken { get; set; }

        /// <summary>
        /// The session identifier for the Klarna payment session
        /// </summary>
        public string SessionId { get; set; }
    }
}
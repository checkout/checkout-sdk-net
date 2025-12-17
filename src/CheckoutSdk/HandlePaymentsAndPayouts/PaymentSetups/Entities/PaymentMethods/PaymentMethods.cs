using System.Collections.Generic;

namespace Checkout.Payments.Setups.Entities
{
    public class PaymentMethods
    {
        /// <summary>
        /// The Klarna payment method's details and configuration.
        /// </summary>
        public Klarna Klarna { get; set; }

        /// <summary>
        /// The stc pay payment method's details and configuration.
        /// </summary>
        public Stcpay Stcpay { get; set; }

        /// <summary>
        /// The Tabby payment method's details and configuration.
        /// </summary>
        public Tabby Tabby { get; set; }

        /// <summary>
        /// The Bizum payment method's details and configuration.
        /// </summary>
        public Bizum Bizum { get; set; }
    }
}
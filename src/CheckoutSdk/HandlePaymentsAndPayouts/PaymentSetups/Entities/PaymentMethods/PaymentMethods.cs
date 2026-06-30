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

        /// <summary>
        /// The PayPal payment method's details and configuration.
        /// </summary>
        public Paypal Paypal { get; set; }

        /// <summary>
        /// The Blik payment method's details and configuration.
        /// </summary>
        public Blik Blik { get; set; }

        /// <summary>
        /// The Bacs payment method's details and configuration.
        /// </summary>
        public Bacs Bacs { get; set; }

        /// <summary>
        /// The Card Present payment method's details and configuration.
        /// </summary>
        public CardPresent CardPresent { get; set; }

        /// <summary>
        /// The Pay by Bank (Open Banking) payment method's details and configuration.
        /// </summary>
        public PayByBank PayByBank { get; set; }

        /// <summary>
        /// The Stablecoin payment method's details and configuration.
        /// </summary>
        public Stablecoin Stablecoin { get; set; }
    }
}
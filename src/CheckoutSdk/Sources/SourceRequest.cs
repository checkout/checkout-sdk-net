using Checkout.Common;
using System;

namespace Checkout.Sources
{
    /// <summary>
    /// Defines a request for a source.
    /// </summary>
    public class SourceRequest
    {
        /// <summary>
        /// Creates a new <see cref="SourceRequest"/> instance.
        /// </summary>
        /// <param name="type">The payment source type.</param>
        /// <param name="billingAddress">The payment source owner's billing address.</param>
        public SourceRequest(string type, Address billingAddress)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("The payment source type is required.", nameof(type));

            Type = type;
            BillingAddress = billingAddress ?? throw new ArgumentNullException(nameof(billingAddress), "The payment source owner's billing address is required.");
        }

        /// <summary>
        /// Gets or sets the type of the source.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets or sets the billing address of the source.
        /// </summary>
        public Address BillingAddress { get; }

        /// <summary>
        /// Gets or sets the reference of the source.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the source.
        /// </summary>
        public Phone Phone { get; set; }

        /// <summary>
        /// Gets or sets the customer of the source.
        /// </summary>
        public CustomerRequest Customer { get; set; }

        /// <summary>
        /// Gets or sets the specific source data of the source.
        /// </summary>
        public SourceData SourceData { get; set; }
    }
}

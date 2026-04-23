using System;
using System.Collections.Generic;

namespace Checkout.Disputes
{
    public class CompellingEvidence
    {
        /// <summary>
        /// Whether the evidence concerns merchandise or services.
        /// [Required]
        /// Enum: "Merchandise" "Services"
        /// </summary>
        public string MerchandiseOrService { get; set; }

        /// <summary>
        /// A description of the merchandise or service provided.
        /// [Required]
        /// min 1 character, max 5000 characters
        /// </summary>
        public string MerchandiseOrServiceDesc { get; set; }

        /// <summary>
        /// The date the merchandise or service was provided to the customer.
        /// [Required]
        /// Format: date-time (RFC 3339)
        /// </summary>
        public DateTime? MerchandiseOrServiceProvidedDate { get; set; }

        /// <summary>
        /// The shipping or delivery status of the merchandise.
        /// [Optional]
        /// </summary>
        public ShippingDeliveryStatusType? ShippingDeliveryStatus { get; set; }

        /// <summary>
        /// The tracking number for the shipment.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public TrackingInformationType? TrackingInformation { get; set; }

        /// <summary>
        /// The customer's account or login identifier.
        /// [Optional]
        /// max 50 characters
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The IP address used for the transaction (IPv4 or IPv6).
        /// [Optional]
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The device identifier used for the transaction.
        /// [Optional]
        /// min 15 characters, max 64 characters
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// The shipping address used for the transaction.
        /// [Optional]
        /// </summary>
        public ShippingAddress ShippingAddress { get; set; }

        /// <summary>
        /// Prior undisputed transactions between the merchant and the same customer.
        /// At least 2 items required.
        /// [Required]
        /// </summary>
        public IList<HistoricalTransactions> HistoricalTransactions { get; set; }
    }
}

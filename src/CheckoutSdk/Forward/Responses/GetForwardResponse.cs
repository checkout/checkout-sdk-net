using System;

namespace Checkout.Forward.Responses
{
    public class GetForwardResponse : HttpMetadata
    {
        /// <summary> The unique identifier for the forward request (Required) </summary>
        public string RequestId { get; set; }

        /// <summary> The client entity linked to the forward request (Required) </summary>
        public string EntityId { get; set; }

        /// <summary> The parameters of the HTTP request forwarded to the destination (Required) </summary>
        public DestinationRequest DestinationRequest { get; set; }

        /// <summary> The date and time the forward request was created, in UTC (Required) </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary> The unique reference for the forward request (Optional) </summary>
        public string Reference { get; set; }

        /// <summary>
        ///     The HTTP response received from the destination. Sensitive PCI data is not included in the response
        ///     (Optional)
        /// </summary>
        public DestinationResponse DestinationResponse { get; set; }
    }
}
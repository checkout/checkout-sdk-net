namespace Checkout.Forward.Responses
{
    public class ForwardAnApiResponse : HttpMetadata
    {
        /// <summary> The unique identifier for the forward request (Required) </summary>
        public string RequestId { get; set; }

        /// <summary>
        ///     The HTTP response received from the destination, if the forward request completed successfully. Sensitive PCI
        ///     data will be removed from the response (Optional)
        /// </summary>
        public DestinationResponse DestinationResponse { get; set; }
    }
}
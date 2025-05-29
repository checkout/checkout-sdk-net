using Checkout.Forward.Requests.Sources;

namespace Checkout.Forward.Requests
{
    public class ForwardRequest
    {
        /// <summary>
        ///     The payment source to enrich the forward request with. You can provide placeholder values in
        ///     destination_request.body. The request will be enriched with the respective payment credentials from the token or
        ///     payment instrument you specified. For example, {{card_number}} (Required)
        /// </summary>
        public AbstractSource Source { get; set; }

        /// <summary> The parameters of the forward request (Required) </summary>
        public DestinationRequest DestinationRequest { get; set; }

        /// <summary> The unique reference for the forward request (Optional, max 80 characters) </summary>
        public string Reference { get; set; }

        /// <summary>
        ///     The processing channel ID to associate the billing for the forward request with (Optional, 
        ///     pattern ^(pc)_(\w{26})$)
        /// </summary>
        public string ProcessingChannelId { get; set; }

        /// <summary> Specifies if and how a network token should be used in the forward request (Optional) </summary>
        public NetworkToken NetworkToken { get; set; }
    }
}
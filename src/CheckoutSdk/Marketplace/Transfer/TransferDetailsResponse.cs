using System;
using System.Collections.Generic;

namespace Checkout.Marketplace.Transfer
{
    public class TransferDetailsResponse
    {
        public string Id { get; set; }

        public string Reference { get; set; }

        public TransferStatus? Status { get; set; }

        public TransferType? TransferType { get; set; }

        public DateTime? RequestedOn { get; set; }

        public IList<string> ReasonCodes { get; set; }

        public TransferSourceResponse Source { get; set; }

        public TransferDestinationResponse Destination { get; set; }
    }
}
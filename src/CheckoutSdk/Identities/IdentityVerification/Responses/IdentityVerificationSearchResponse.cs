using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Identities.IdentityVerification.Responses
{
    public class IdentityVerificationSearchResponse : Resource
    {
        public int TotalCount { get; set; }

        public int Limit { get; set; }

        public int Skip { get; set; }

        public List<IdentityVerificationResponse> Data { get; set; }
    }
}
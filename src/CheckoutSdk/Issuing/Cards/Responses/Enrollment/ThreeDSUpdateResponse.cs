using Checkout.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Enrollment
{
    public class ThreeDSUpdateResponse : Resource
    {
        public DateTime? LastModifiedDate { get; set; }
    }
}
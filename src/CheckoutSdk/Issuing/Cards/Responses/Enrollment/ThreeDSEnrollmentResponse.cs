using Checkout.Common;
using System;

namespace Checkout.Issuing.Cards.Responses.Enrollment
{
    public class ThreeDSEnrollmentResponse : Resource
    {
        public DateTime? CreatedDate { get; set; }
    }
}
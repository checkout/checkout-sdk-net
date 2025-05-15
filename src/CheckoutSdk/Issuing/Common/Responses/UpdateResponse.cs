using Checkout.Common;
using System;

namespace Checkout.Issuing.Common.Responses
{
    public class UpdateResponse : Resource
    {
        public DateTime? LastModifiedDate { get; set; }
    }
}
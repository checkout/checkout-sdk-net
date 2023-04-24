using Checkout.Common;
using System;

namespace Checkout.Issuing.Cardholders
{
    public class CardholderResponse : Resource
    {
        public string Id { get; set; }

        public CardholderType? Type { get; set; }

        public CardholderStatus? Status { get; set; }

        public string Reference { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
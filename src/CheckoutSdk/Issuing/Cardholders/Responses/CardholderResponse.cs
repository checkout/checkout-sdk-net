using Checkout.Common;
using Checkout.Issuing.Cardholders.Requests;
using System;

namespace Checkout.Issuing.Cardholders.Responses
{
    public class CardholderResponse : Resource
    {
        public string Id { get; set; }
        
        public string ClientId { get; set; }
        
        public string EntityId { get; set; }

        public CardholderType? Type { get; set; }

        public CardholderStatus? Status { get; set; }

        public string Reference { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
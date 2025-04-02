using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Issuing.Transactions.Responses.Query
{
    public class TransactionSingleResponse : Resource
    {
        public string Id { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        
        public TransactionStatusType? Status { get; set; }
        
        public TransactionType? TransactionType { get; set; }
        
        public Client Client { get; set; }
        
        public Entity Entity { get; set; }
        
        public Card Card { get; set; }
        
        public DigitalCard DigitalCard { get; set; }
        
        public Cardholder Cardholder { get; set; }
        
        public Amounts Amounts { get; set; }
        
        public Merchant Merchant { get; set; }
        
        public ReferenceTransaction ReferenceTransaction { get; set; }
        
        public IList<Messages> Messages { get; set; }
    }
}
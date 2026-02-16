using System;
using Checkout.Accounts;

namespace Checkout.Accounts.ReserveRules
{
    public class ReserveRuleRequest : ReserveRuleBase
    {
        public Headers Headers { get; set; }
    }
}
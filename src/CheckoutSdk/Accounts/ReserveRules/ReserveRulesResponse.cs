using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.ReserveRules
{
    public class ReserveRulesResponse : Resource
    {
        public IList<ReserveRuleResponse> Data { get; set; }
    }
}
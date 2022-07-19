using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts
{
    public class Profile
    {
        public IList<string> Urls { get; set; }

        public IList<string> Mccs { get; set; }

        public Currency? DefaultHoldingCurrency { get; set; }
    }
}
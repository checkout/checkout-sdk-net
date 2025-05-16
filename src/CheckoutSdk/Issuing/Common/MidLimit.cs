using System.Collections.Generic;

namespace Checkout.Issuing.Common
{
    public class MidLimit
    {
        public LimitControlType? Type { get; set; }

        public IList<string> MidList { get; set; }
    }
}
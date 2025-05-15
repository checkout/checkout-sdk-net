using System.Collections.Generic;

namespace Checkout.Issuing.Common
{
    public class MccLimit
    {
        public LimitControlType? Type { get; set; }

        public IList<string> MccList { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Issuing.Common
{
    public class VelocityLimit
    {
        public long? AmountLimit { get; set; }

        public VelocityWindow VelocityWindow { get; set; }

        public IList<string> MccList { get; set; }
        
        public IList<string> MidList { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Issuing.Controls.Requests
{
    public class VelocityLimit
    {
        public int? AmountLimit { get; set; }

        public VelocityWindow VelocityWindow { get; set; }

        public IList<string> MccList { get; set; }
    }
}
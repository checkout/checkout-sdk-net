using System.Collections.Generic;

namespace Checkout.Issuing.Controls.Requests
{
    public class MccLimit
    {
        public MccControlType? Type { get; set; }

        public IList<string> MccList { get; set; }
    }
}
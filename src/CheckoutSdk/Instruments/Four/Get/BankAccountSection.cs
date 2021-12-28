using System.Collections.Generic;

namespace Checkout.Instruments.Four.Get
{
    public class BankAccountSection
    {
        public string Name { get; set; }

        public IList<BankAccountField> Fields { get; set; }
    }
}
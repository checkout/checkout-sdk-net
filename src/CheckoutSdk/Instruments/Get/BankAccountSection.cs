using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    public class BankAccountSection
    {
        public string Name { get; set; }

        public IList<BankAccountField> Fields { get; set; }
    }
}
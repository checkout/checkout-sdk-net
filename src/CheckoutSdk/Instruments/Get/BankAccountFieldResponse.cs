using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    public class BankAccountFieldResponse : HttpMetadata
    {
        public IList<BankAccountSection> Sections { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Apm.Ideal
{
    public sealed class IdealCountry
    {
        public string Name { get; set; }

        public IList<Issuer> Issuers { get; set; }
    }
}
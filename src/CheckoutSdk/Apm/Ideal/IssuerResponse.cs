using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Apm.Ideal
{
    public sealed class IssuerResponse : Resource
    {
        public IList<IdealCountry> Countries { get; set; }
    }
}
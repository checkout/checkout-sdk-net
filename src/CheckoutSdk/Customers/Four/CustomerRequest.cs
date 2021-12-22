using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Customers.Four
{
    public sealed class CustomerRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

        public IList<string> Instruments { get; set; }
    }
}
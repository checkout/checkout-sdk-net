using Checkout.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Customers.Previous
{
    public class CustomerRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public Phone Phone { get; set; }

        public IDictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
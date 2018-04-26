using System;
using System.Collections.Generic;

namespace Checkout.Payments
{
    public class AlternativePaymentSource : Dictionary<string, object>
    {
        private const string TypeKey = "type";

        public AlternativePaymentSource(string type, Dictionary<string, object> properties = null)
        {
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));
            Type = type;

            if (properties != null)
            {
                // merge
            }
        }

        public string Type
        {
            get { return this[TypeKey].ToString(); }
            private set { this[TypeKey] = value; }
        }
    }
}
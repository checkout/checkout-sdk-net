using System;

namespace Checkout.Payments
{
    public class GiroPaySource : PaymentSource
    {
        public GiroPaySource()
            : base("giropay")
        {
        }
    }
}
using Checkout.Common;
using System;

namespace Checkout.Payments.Contexts
{
    public class PaymentContextsPassenger
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Address Address { get; set; }
    }
}
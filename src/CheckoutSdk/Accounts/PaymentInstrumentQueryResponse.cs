using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts
{
    public class PaymentInstrumentQueryResponse : Resource
    {
        public IList<PaymentInstrumentDetailsResponse> Data { get; set; }
    }
}
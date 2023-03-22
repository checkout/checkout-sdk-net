using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Request.Source.Apm
{
    public class RequestGiropaySource : AbstractRequestSource
    {
        public RequestGiropaySource() : base(PaymentSourceType.Giropay)
        {
        }

        [Obsolete("GiroPay doesn't support this field anymore, will be removed in the future", false)]
        public string Purpose { get; set; }

        [Obsolete("GiroPay doesn't support this field anymore, will be removed in the future", false)]
        public IList<InfoField> InfoFields { get; set; }

        public class InfoField
        {
            public string Label { get; set; }

            public string Text { get; set; }
        }
    }
}
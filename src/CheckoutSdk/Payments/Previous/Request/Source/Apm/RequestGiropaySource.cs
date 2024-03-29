﻿using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestGiropaySource : AbstractRequestSource
    {
        public RequestGiropaySource() : base(PaymentSourceType.Giropay)
        {
        }

        public string Purpose { get; set; }

        public string Bic { get; set; }

        public IList<InfoField> InfoFields { get; set; }

        public class InfoField
        {
            public string Label { get; set; }

            public string Text { get; set; }
        }
    }
}
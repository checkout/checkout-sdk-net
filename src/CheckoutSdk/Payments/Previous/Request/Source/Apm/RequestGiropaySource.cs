﻿using Checkout.Common;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestGiropaySource : AbstractRequestSource
    {
        public RequestGiropaySource() : base(PaymentSourceType.Giropay)
        {
        }

        public string Purpose { get; set; }

        public string Bic { get; set; }
    }
}
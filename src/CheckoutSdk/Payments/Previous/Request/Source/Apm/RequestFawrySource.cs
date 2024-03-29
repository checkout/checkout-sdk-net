﻿using Checkout.Common;
using Checkout.Payments.Request.Source.Apm;
using System;
using System.Collections.Generic;

namespace Checkout.Payments.Previous.Request.Source.Apm
{
    public class RequestFawrySource : AbstractRequestSource
    {
        public RequestFawrySource() : base(PaymentSourceType.Fawry)
        {
        }

        public string Description { get; set; }

        public string CustomerProfileId { get; set; }

        public string CustomerMobile { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public IList<FawryProduct> Products { get; set; }
    }
}
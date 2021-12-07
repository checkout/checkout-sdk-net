using System;
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class RequestFawrySource : AbstractRequestSource, IEquatable<RequestFawrySource>
    {
        public RequestFawrySource() : base(PaymentSourceType.Fawry)
        {
        }

        public string Description { get; set; }

        public string CustomerMobile { get; set; }

        public string CustomerEmail { get; set; }

        public IList<FawryProduct> Products { get; set; }

        public bool Equals(RequestFawrySource other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Description == other.Description && CustomerMobile == other.CustomerMobile &&
                   CustomerEmail == other.CustomerEmail && Equals(Products, other.Products);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RequestFawrySource other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Description, CustomerMobile, CustomerEmail, Products);
        }
    }
}
using System;

namespace Checkout.Payments.Request.Source.Apm
{
    public sealed class FawryProduct 
    {
        public string ProductId { get; set; }

        public long? Quantity { get; set; }

        public long? Price { get; set; }

        public string Description { get; set; }

    }
}
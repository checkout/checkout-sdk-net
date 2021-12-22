using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Reconciliation
{
    public sealed class Action
    {
        public string Id { get; set; }

        public ActionType? Type { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public long? ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public IList<Breakdown> Breakdown { get; set; }
    }
}
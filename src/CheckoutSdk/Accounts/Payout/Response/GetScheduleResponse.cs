using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Accounts.Payout.Response
{
    public class GetScheduleResponse : Resource
    {
        public IDictionary<Currency, CurrencySchedule> Currency { get; set; } = new Dictionary<Currency, CurrencySchedule>();
    }
}
namespace Checkout.Accounts.Payout.Response
{
    public class CurrencySchedule
    {
        public bool? Enabled { get; set; }

        public int? Threshold { get; set; }

        /// <summary>
        /// The ID of the platforms payment instrument this schedule pays out to.
        /// </summary>
        public string PaymentInstrumentId { get; set; }

        /// <summary>
        /// The amount, in the minor units of the schedule's currency, retained in the
        /// sub-entity's available balance. Only the funds above this are paid out.
        /// Returned for SaaS seller (ISV) schedules.
        /// </summary>
        public long? BalanceMinimum { get; set; }

        /// <summary>
        /// Whether a balance below the configured minimum is carried forward to the next payout.
        /// Always returned for SaaS sellers, where it defaults to false.
        /// </summary>
        public bool? CarryForwardEnabled { get; set; }

        public ScheduleResponse Recurrence { get; set; }
    }
}

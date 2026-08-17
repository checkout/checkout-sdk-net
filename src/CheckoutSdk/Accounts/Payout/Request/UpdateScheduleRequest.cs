namespace Checkout.Accounts.Payout.Request
{
    public class UpdateScheduleRequest
    {
        public bool? Enabled { get; set; }

        public int? Threshold { get; set; }

        /// <summary>
        /// The ID of the platforms payment instrument to pay out to on this schedule.
        /// Optional for SaaS seller (ISV) schedules, but when supplied it must reference a
        /// verified payment instrument, otherwise the request is rejected.
        /// </summary>
        public string PaymentInstrumentId { get; set; }

        /// <summary>
        /// The amount, in the minor units of the schedule's currency, to retain in the
        /// sub-entity's available balance. Only the funds above this are paid out, and no payout
        /// is generated if there are none. Defaults to 0 when not set.
        /// SaaS seller (ISV) schedules only.
        /// </summary>
        public long? BalanceMinimum { get; set; }

        /// <summary>
        /// Whether to carry forward to the next payout any balance below the configured minimum.
        /// Defaults to false when not set.
        /// SaaS seller (ISV) schedules only.
        /// </summary>
        public bool? CarryForwardEnabled { get; set; }

        public ScheduleRequest Recurrence { get; set; }
    }
}

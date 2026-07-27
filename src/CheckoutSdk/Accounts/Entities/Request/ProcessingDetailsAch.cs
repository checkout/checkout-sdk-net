namespace Checkout.Accounts.Entities.Request
{
    public class ProcessingDetailsAch
    {
        /// <summary>
        /// The estimated annual ACH processing volume in minor units without decimals.
        /// </summary>
        public int? AnnualAchVolume { get; set; }

        /// <summary>
        /// The expected average ACH transaction size in minor units without decimals.
        /// </summary>
        public int? AverageAchTransactionSize { get; set; }

        /// <summary>
        /// The estimated monthly volume of ACH credit transactions (for example, refunds issued to
        /// customers) in minor units without decimals.
        /// </summary>
        public int? EstimatedMonthlyCreditVolume { get; set; }

        /// <summary>
        /// The average value of an ACH credit transaction (for example, a refund) in minor units
        /// without decimals.
        /// </summary>
        public int? AverageCreditAmount { get; set; }
    }
}

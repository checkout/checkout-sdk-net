namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Agentic Statistics (detailed version)
    /// Contains comprehensive statistics for detailed views
    /// </summary>
    public class AgenticStatistics : AgenticStatisticsSummary
    {
        /// <summary>
        /// Total number of autonomous payments processed
        /// </summary>
        public int TotalAutonomousPayments { get; set; }

        /// <summary>
        /// Total value of autonomous payments
        /// </summary>
        public decimal TotalAutonomousPaymentValue { get; set; }

        /// <summary>
        /// Average processing time in milliseconds
        /// </summary>
        public double AverageProcessingTime { get; set; }

        // Inherited properties from AgenticStatisticsBase:
        // - TotalAutonomousActions
        // - SuccessRate
        // - LastAutonomousAction
    }
}
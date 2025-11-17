using System;

namespace Checkout.Agentic.Responses
{
    /// <summary>
    /// Base class for Agentic Commerce statistics
    /// </summary>
    public abstract class AgenticStatisticsSummary
    {
        /// <summary>
        /// Total number of autonomous actions performed
        /// </summary>
        public int TotalAutonomousActions { get; set; }

        /// <summary>
        /// Success rate of autonomous actions
        /// </summary>
        public decimal SuccessRate { get; set; }

        /// <summary>
        /// The timestamp of the last autonomous action
        /// </summary>
        public DateTime? LastAutonomousAction { get; set; }
    }
}
using System.Collections.Generic;
using Checkout.Common;

namespace Checkout.Accounts.Simulator
{
    /// <summary>
    /// Error response from the Onboarding Simulator.
    /// </summary>
    public class SimulatorErrorResponse : Resource
    {
        /// <summary>
        /// Error codes describing what went wrong.
        /// [Optional]
        /// </summary>
        public IList<string> ErrorCodes { get; set; }
    }
}

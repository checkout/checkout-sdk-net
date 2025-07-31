using System.Collections.Generic;

namespace Checkout.Authentication.Standalone.Common.Responses.Optimization
{
    /// <summary>
    /// optimization
    /// The information about the optimization options selected
    /// </summary>
    public class Optimization
    {
        /// <summary>
        /// Indicates if any optimization has been applied
        /// [Optional]
        /// </summary>
        public bool Optimized { get; set; }

        /// <summary>
        /// The optimization framework applied
        /// [Optional]
        /// </summary>
        public string Framework { get; set; }

        /// <summary>
        /// The collection of fields optimized
        /// [Optional]
        /// </summary>
        public IList<OptimizedProperties.OptimizedProperties> OptimizedProperties { get; set; }
    }
}
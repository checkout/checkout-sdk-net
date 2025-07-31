namespace Checkout.Authentication.Standalone.Common.Responses.Optimization.OptimizedProperties
{
    /// <summary>
    /// optimized_properties
    /// The collection of fields optimized
    /// </summary>
    public class OptimizedProperties
    {
        /// <summary>
        /// Name of the field which has been optimized.
        /// [Optional]
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Value prior to optimization.
        /// [Optional]
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// Value prior to optimization.
        /// [Optional]
        /// </summary>
        public string OptimizedValue { get; set; }
    }
}
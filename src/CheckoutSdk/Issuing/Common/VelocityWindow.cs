namespace Checkout.Issuing.Common
{
    /// <summary>
    /// The period of time over which the specified amount_limit can be spent.
    /// </summary>
    public class VelocityWindow
    {
        /// <summary>
        /// The velocity window's unit of time.
        /// [Required]
        /// </summary>
        public VelocityWindowType? Type { get; set; }
    }
}

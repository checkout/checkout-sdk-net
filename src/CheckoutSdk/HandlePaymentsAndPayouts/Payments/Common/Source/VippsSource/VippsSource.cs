namespace Checkout.HandlePaymentsAndPayouts.Payments.Common.Source.
    VippsSource
{
    /// <summary>
    /// vipps source Class
    /// The source of the payment
    /// </summary>
    public class VippsSource : AbstractSource
    {
        /// <summary>
        /// Initializes a new instance of the VippsSource class.
        /// </summary>
        public VippsSource() : base(SourceType.Vipps)
        {
        }
    }
}
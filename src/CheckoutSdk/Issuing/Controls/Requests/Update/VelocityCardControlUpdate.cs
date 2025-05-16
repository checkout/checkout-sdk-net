using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Update
{
    public class VelocityCardControlUpdate : AbstractCardControlUpdate
    {
        public VelocityCardControlUpdate() : base(IssuingControlType.VelocityLimit)
        {
        }

        public VelocityLimit VelocityLimit { get; set; }
    }
}
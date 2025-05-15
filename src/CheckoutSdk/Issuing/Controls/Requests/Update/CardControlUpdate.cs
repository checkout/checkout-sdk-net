using Checkout.Issuing.Common;

namespace Checkout.Issuing.Controls.Requests.Update
{
    public abstract class AbstractCardControlUpdate
    {
        public IssuingControlType Type { get; set; }
        
        public string Description { get; set; }
        
        protected AbstractCardControlUpdate(IssuingControlType type)
        {
            Type = type;
        }
    }
}
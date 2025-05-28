using Checkout.Payments.Request;

namespace Checkout.Payments
{
    public class RiskRequest
    {
        /// <summary> Whether a risk assessment should be performed (Optional) </summary>
        public bool? Enabled { get; set; } = true;

        /// <summary> Device session ID collected from our standalone Risk.js package. If you integrate using our Frames
        /// solution, this ID is not required (Optional, pattern ^(dsid)_(\w{26})$) </summary>
        public string DeviceSessionId { get; set; }
        
        /// <summary> Details of the device from which the payment originated (Optional) </summary>
        public Device Device { get; set; }
    }
}
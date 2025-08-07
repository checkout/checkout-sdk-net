namespace Checkout.Authentication.Standalone.POSTSessions.Requests.RequestASessionRequest.BillingDescriptor
{
    /// <summary>
    /// billing_descriptor
    /// An optional dynamic billing descriptor.
    /// </summary>
    public class BillingDescriptor
    {
        /// <summary>
        /// A dynamic description of the payment that replaces the Merchant Name that is displayed in 3DS Challenge
        /// window. Applies to card payments only. Special characters allowed: . ! * - = _ @
        /// [Optional]
        /// <= 40
        /// </summary>
        public string Name { get; set; }
    }
}
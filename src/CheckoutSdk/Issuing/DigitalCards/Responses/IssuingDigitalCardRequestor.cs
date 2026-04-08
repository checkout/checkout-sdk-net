namespace Checkout.Issuing.DigitalCards.Responses
{
    /// <summary>
    /// The entity that requested the digital card provisioning.
    /// </summary>
    public class IssuingDigitalCardRequestor
    {
        /// <summary>The requestor identifier.</summary>
        public string Id { get; set; }

        /// <summary>The requestor name.</summary>
        public string Name { get; set; }
    }
}

namespace Checkout.Issuing.ControlProfiles.Requests
{
    /// <summary>
    /// Query parameters for retrieving a list of control profiles applied to the specified card.
    /// </summary>
    public class ControlProfileQueryTarget
    {
        /// <summary>
        /// The card's unique identifier.
        /// ^crd_[a-z0-9]{26}$
        /// 30 characters
        /// [Optional]
        /// </summary>
        public string TargetId { get; set; }
    }
}

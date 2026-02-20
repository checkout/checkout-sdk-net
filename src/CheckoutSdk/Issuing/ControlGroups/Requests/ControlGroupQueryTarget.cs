namespace Checkout.Issuing.ControlGroups.Requests
{
    /// <summary>
    /// Query parameters for retrieving a list of control groups applied to the specified target.
    /// </summary>
    public class ControlGroupQueryTarget
    {
        /// <summary>
        /// The ID of the card or control profile.
        /// ^(crd|cpr)_[a-z0-9]{26}$
        /// 30 characters
        /// [Required]
        /// </summary>
        public string TargetId { get; set; }
    }
}
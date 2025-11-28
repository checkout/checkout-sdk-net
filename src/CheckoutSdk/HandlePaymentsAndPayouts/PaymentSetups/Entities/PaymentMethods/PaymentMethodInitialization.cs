
    using System.Runtime.Serialization;
    
    public enum PaymentMethodInitialization
    {
        /// <summary>
        /// The Klarna payment method is disabled
        /// </summary>
         [EnumMember(Value = "disabled")]
        Disabled,
        
        /// <summary>
        /// The Klarna payment method is enabled
        /// </summary>
        [EnumMember(Value = "enabled")]
        Enabled
    }
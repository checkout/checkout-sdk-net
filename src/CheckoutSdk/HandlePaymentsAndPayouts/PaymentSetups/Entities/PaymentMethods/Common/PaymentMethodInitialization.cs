
    using System.Runtime.Serialization;
    
    public enum PaymentMethodInitialization
    {
        /// <summary>
        /// The payment method is disabled
        /// </summary>
         [EnumMember(Value = "disabled")]
        Disabled,
        
        /// <summary>
        /// The payment method is enabled
        /// </summary>
        [EnumMember(Value = "enabled")]
        Enabled
    }
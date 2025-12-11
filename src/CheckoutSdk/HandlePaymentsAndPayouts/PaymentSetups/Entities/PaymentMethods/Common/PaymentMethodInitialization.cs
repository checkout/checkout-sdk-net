
    using System.Runtime.Serialization;
    
    public enum PaymentMethodInitialization
    {
        [EnumMember(Value = "disabled")]
        Disabled,
        
        [EnumMember(Value = "enabled")]
        Enabled
    }
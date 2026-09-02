using System.Runtime.Serialization;

namespace Checkout.Payments.Request.Source.Apm
{
    /// <summary>
    /// The type of Direct Debit account on an ACH payment source.
    /// PaymentRequestAchSource is the only schema declaring this set of values.
    /// Two neighbouring enums are deliberately different and are not interchangeable:
    /// Checkout.Common.AccountType is savings / current / cash and serves the
    /// bank-account instrument and destination positions, so it cannot express
    /// "checking"; Checkout.Instruments.AchAccountType is savings / checking and
    /// serves the stored ACH instrument positions, so it does not declare "cash".
    /// </summary>
    public enum AchSourceAccountType
    {
        [EnumMember(Value = "savings")]
        Savings,

        [EnumMember(Value = "checking")]
        Checking,

        [EnumMember(Value = "cash")]
        Cash
    }
}

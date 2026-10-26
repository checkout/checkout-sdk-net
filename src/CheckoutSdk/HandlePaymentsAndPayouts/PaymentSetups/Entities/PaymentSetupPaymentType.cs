using System.Runtime.Serialization;

namespace Checkout.Payments.Setups.Entities
{
    /// <summary>
    /// The type of payment of a Payment Setup. The Payment Setups API uses lowercase values, unlike
    /// Checkout.Payments.PaymentType, which belongs to the payments API.
    /// </summary>
    public enum PaymentSetupPaymentType
    {
        [EnumMember(Value = "regular")]
        Regular,

        [EnumMember(Value = "recurring")]
        Recurring,

        [EnumMember(Value = "moto")]
        Moto,

        [EnumMember(Value = "installment")]
        Installment,

        [EnumMember(Value = "pay_later")]
        PayLater,

        [EnumMember(Value = "unscheduled")]
        Unscheduled
    }
}

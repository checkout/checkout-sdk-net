using System.Runtime.Serialization;

namespace Checkout.Issuing.Transactions.Responses
{
    public enum TransactionType
    {
        [EnumMember(Value = "account_funding")]
        AccountFunding,

        [EnumMember(Value = "account_transfer")]
        AccountTransfer,

        [EnumMember(Value = "atm_installment")]
        AtmInstallment,

        [EnumMember(Value = "balance_inquiry")]
        BalanceInquiry,

        [EnumMember(Value = "bill_payment")]
        BillPayment,

        [EnumMember(Value = "cash_advance")]
        CashAdvance,

        [EnumMember(Value = "cashback")]
        Cashback,

        [EnumMember(Value = "credit_adjustment")]
        CreditAdjustment,

        [EnumMember(Value = "debit_adjustment")]
        DebitAdjustment,

        [EnumMember(Value = "original_credit")]
        OriginalCredit,

        [EnumMember(Value = "payment_account_inquiry")]
        PaymentAccountInquiry,

        [EnumMember(Value = "payment")]
        Payment,

        [EnumMember(Value = "pin_change")]
        PinChange,

        [EnumMember(Value = "pin_unblock")]
        PinUnblock,

        [EnumMember(Value = "purchase_account_inquiry")]
        PurchaseAccountInquiry,

        [EnumMember(Value = "purchase")]
        Purchase,

        [EnumMember(Value = "quasi_cash")]
        QuasiCash,

        [EnumMember(Value = "remittance_funding")]
        RemittanceFunding,

        [EnumMember(Value = "remittance_payment")]
        RemittancePayment,

        [EnumMember(Value = "unknown")]
        Unknown,

        [EnumMember(Value = "withdrawal")]
        Withdrawal,

        [EnumMember(Value = "refund")]
        Refund
    }
}
using System.Runtime.Serialization;

namespace Checkout.HandlePaymentsAndPayouts.Payments.POSTPayments.Requests.UnreferencedRefundRequest.Instruction
{
    public enum PurposeType
    {
        [EnumMember(Value = "family_support")]
        FamilySupport,

        [EnumMember(Value = "expatriation")]
        Expatriation,

        [EnumMember(Value = "travel_and_tourism")]
        TravelAndTourism,

        [EnumMember(Value = "education")]
        Education,

        [EnumMember(Value = "medical_treatment")]
        MedicalTreatment,

        [EnumMember(Value = "emergency_need")]
        EmergencyNeed,

        [EnumMember(Value = "leisure")]
        Leisure,

        [EnumMember(Value = "savings")]
        Savings,

        [EnumMember(Value = "gifts")]
        Gifts,

        [EnumMember(Value = "donations")]
        Donations,

        [EnumMember(Value = "financial_services")]
        FinancialServices,

        [EnumMember(Value = "it_services")]
        ItServices,

        [EnumMember(Value = "investment")]
        Investment,

        [EnumMember(Value = "insurance")]
        Insurance,

        [EnumMember(Value = "loan_payment")]
        LoanPayment,

        [EnumMember(Value = "pension")]
        Pension,

        [EnumMember(Value = "royalties")]
        Royalties,

        [EnumMember(Value = "other")]
        Other,

        [EnumMember(Value = "income")]
        Income,
    }
}
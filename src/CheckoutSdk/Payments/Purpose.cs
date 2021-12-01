﻿using System.Runtime.Serialization;

namespace Checkout.Payments
{
    public enum Purpose
    {
        [EnumMember(Value = "donations")] Donations,
        [EnumMember(Value = "education")] Education,
        [EnumMember(Value = "emergency_need")] EmergencyNeed,
        [EnumMember(Value = "expatriation")] Expatriation,
        [EnumMember(Value = "family_support")] FamilySupport,
        [EnumMember(Value = "gifts")] Gifts,
        [EnumMember(Value = "income")] Income,
        [EnumMember(Value = "insurance")] Insurance,
        [EnumMember(Value = "investment")] Investment,
        [EnumMember(Value = "it_services")] ItServices,
        [EnumMember(Value = "leisure")] Leisure,
        [EnumMember(Value = "loan_payment")] LoanPayment,
        [EnumMember(Value = "other")] Other,
        [EnumMember(Value = "pension")] Pension,
        [EnumMember(Value = "royalties")] Royalties,
        [EnumMember(Value = "savings")] Savings,

        [EnumMember(Value = "travel_and_tourism")]
        TravelAndTourism,

        [EnumMember(Value = "financial_services")]
        FinancialServices,

        [EnumMember(Value = "medical_treatment")]
        MedicalTreatment
    }
}
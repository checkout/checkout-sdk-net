

namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public class Instruction
    {
        /// <summary>
        /// The purpose of the payment.
        /// </summary>
        public Purpose? Purpose { get; set; }
    }

    public enum Purpose
    {
        Donations,
        
        Education,
        
        EmergencyNeed,
        
        Expatriation,
        
        FamilySupport,
        
        FinancialServices,
        
        Gifts,
        
        Income,
        
        Insurance,
        
        Investment,
        
        ItServices,
        
        Leisure,
        
        LoanPayment,
        
        MedicalTreatment,
        
        Other,
        
        Pension,
        
        Royalties,
        
        Savings,
        
        TravelAndTourism
    }
}
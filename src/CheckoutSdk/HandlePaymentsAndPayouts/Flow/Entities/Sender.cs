using Checkout.Common;


namespace Checkout.HandlePaymentsAndPayouts.Flow.Entities
{
    public abstract class AbstractSender
    {
        /// <summary>
        /// The sender type
        /// </summary>
        public abstract SenderType? Type { get; set; }

        /// <summary>
        /// The sender's reference for the payout
        /// </summary>
        public string Reference { get; set; }
    }

    public class IndividualSender : AbstractSender
    {
        /// <summary>
        /// The sender type
        /// </summary>
        public override SenderType? Type { get; set; } = SenderType.Individual;

        /// <summary>
        /// The sender's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The sender's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The sender's date of birth, in the format yyyy-mm-dd.
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// The sender's address
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Identification details
        /// </summary>
        public SenderIdentification Identification { get; set; }
    }

    public class CorporateSender : AbstractSender
    {
        /// <summary>
        /// The type of entity performing the payment
        /// </summary>
        public override SenderType? Type { get; set; } = SenderType.Corporate;

        /// <summary>
        /// The corporate sender's company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The sender's registered corporate address.
        /// </summary>
        public Address Address { get; set; }
    }

    public class InstrumentSender : AbstractSender
    {
        /// <summary>
        /// The type of entity performing the payment
        /// </summary>
        public override SenderType? Type { get; set; } = SenderType.Instrument;
    }

    public class SenderIdentification
    {
        /// <summary>
        /// The type of identification used to identify the sender
        /// </summary>
        public IdentificationType? Type { get; set; }

        /// <summary>
        /// The identification number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The two-letter ISO country code of the country that issued the identification
        /// </summary>
        public string IssuingCountry { get; set; }
    }

    public enum SenderType
    {
        Individual,
        
        Corporate,
        
        Instrument
    }

    public enum IdentificationType
    {
        Passport,
        
        DrivingLicence,
        
        NationalId
    }
}
using Checkout.Common;

namespace Checkout.Identities.Common
{
    public class VerifiedIdentity
    {
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string LastNameAtBirth { get; set; }
        public string BirthPlace { get; set; }
        public CountryCode? Nationality { get; set; }
        public Gender? Gender { get; set; }
    }
}
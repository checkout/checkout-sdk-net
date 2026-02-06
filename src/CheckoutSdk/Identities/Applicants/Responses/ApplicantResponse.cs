using System;
using Checkout.Common;
using Checkout.Identities.Common;

namespace Checkout.Identities.Applicants.Responses
{
    public class ApplicantResponse : Resource
    {
        public string Id { get; set; }

        public string ExternalApplicantId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public CountryCode? Nationality { get; set; }

        public Phone Phone { get; set; }

        public Address Address { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
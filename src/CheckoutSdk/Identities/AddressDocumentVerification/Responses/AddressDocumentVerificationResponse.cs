using System.Collections.Generic;
using Checkout.Identities.Entities;
using Checkout.Identities.Entities.Responses;

namespace Checkout.Identities.AddressDocumentVerification.Responses
{
    public class AddressDocumentVerificationResponse : BaseVerificationResponse<AddressDocumentVerificationStatus>
    {
        /// <summary>
        /// The personal details provided by the applicant
        /// </summary>
        public DeclaredData DeclaredData { get; set; }

        /// <summary>
        /// The result of the address document check
        /// </summary>
        public AddressDocumentResult AddressDocument { get; set; }

        /// <summary>
        /// One or more codes that provide more information about risks associated with the
        /// verification.
        /// [Optional]
        /// </summary>
        public List<RiskLabel> RiskLabels { get; set; }
    }
}

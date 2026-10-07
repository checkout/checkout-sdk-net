using System;

namespace Checkout.Accounts.Entities.Common.Documents
{
    /// <summary>
    /// Certified authorised signatory document. Required when the legal representative or other role
    /// owner is not registered on the certificate of incorporation. Representative documents only
    /// (<c>company.representatives[].documents</c>), EEA, GB and US Company Full (3.0) and US ISV
    /// Seller Company (3.0); not accepted at the top level.
    /// </summary>
    public class CertifiedAuthorisedSignatory
    {
        /// <summary>
        /// The type of document.
        /// [Required]
        /// </summary>
        public CertifiedAuthorisedSignatoryType? Type { get; set; }

        /// <summary>
        /// The ID of the front side of the document as represented within Checkout.com systems.
        /// [Required]
        /// ^file_[a-z2-7]{26}$
        /// 31 characters
        /// </summary>
        public string Front { get; set; }

        /// <summary>
        /// Not defined by the Accounts API: the certified authorised signatory document has a front
        /// side only. Retained so existing code keeps compiling.
        /// </summary>
        [Obsolete("Not defined by the Accounts API: certified_authorised_signatory has type and front only. Will be removed in a future major version.")]
        public string Back { get; set; }
    }
}
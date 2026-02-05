using Checkout.Common;
using Checkout.StandaloneAccountUpdater.Entities;

namespace Checkout.StandaloneAccountUpdater.Responses
{
    public class GetUpdatedCardCredentialsResponse : Resource
    {
        /// <summary>
        /// Result of the update operation.
        /// [Required]
        /// </summary>
        /// [Required]
        public AccountUpdateStatus? AccountUpdateStatus { get; set; }

        /// <summary>
        /// This field is returned when the update fails and the scheme returns an appropriate reason code.
        /// For more information, see Standalone Account Updater
        /// </summary>
        public AccountUpdateFailureCode? AccountUpdateFailureCode { get; set; }

        /// <summary>
        /// Updated card details. Fields vary depending on PCI compliance level.
        /// </summary>
        public CardUpdated Card { get; set; }
    }
}
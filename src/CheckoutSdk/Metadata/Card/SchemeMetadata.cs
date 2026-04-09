using System.Collections.Generic;

namespace Checkout.Metadata.Card
{
    public class SchemeMetadata
    {
        /// <summary>
        /// PINless debit network metadata for the Accel network.
        /// </summary>
        public IList<PinlessDebitSchemeMetadata> Accel { get; set; }

        /// <summary>
        /// PINless debit network metadata for the Pulse network.
        /// </summary>
        public IList<PinlessDebitSchemeMetadata> Pulse { get; set; }

        /// <summary>
        /// PINless debit network metadata for the NYCE network.
        /// </summary>
        public IList<PinlessDebitSchemeMetadata> Nyce { get; set; }

        /// <summary>
        /// PINless debit network metadata for the Shazam network.
        /// </summary>
        public IList<PinlessDebitSchemeMetadata> Shazam { get; set; }

        /// <summary>
        /// PINless debit network metadata for the Star network.
        /// </summary>
        public IList<PinlessDebitSchemeMetadata> Star { get; set; }
    }
}

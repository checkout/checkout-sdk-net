using System.Collections.Generic;

namespace Checkout.Metadata.Card
{
    public class SchemeMetadata
    {
        public IList<PinlessDebitSchemeMetadata> Accel { get; set; }
        public IList<PinlessDebitSchemeMetadata> Pulse { get; set; }
        public IList<PinlessDebitSchemeMetadata> Nyce { get; set; }
        public IList<PinlessDebitSchemeMetadata> Star { get; set; }
    }
}
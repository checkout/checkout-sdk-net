using Checkout.Common;
using Checkout.Instruments.Create;
using System;

namespace Checkout.Instruments.Get
{
    public class GetSepaInstrumentResponse : GetInstrumentResponse
    {
        public GetSepaInstrumentResponse() : base(InstrumentType.Sepa)
        {
        }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string VaultId { get; set; }
        
        public InstrumentData InstrumentData { get; set; }

    }
}
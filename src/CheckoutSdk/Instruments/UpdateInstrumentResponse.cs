﻿namespace Checkout.Instruments
{
    public class UpdateInstrumentResponse : HttpMetadata
    {
        public InstrumentType? Type { get; set; }

        public string Fingerprint { get; set; }
    }
}
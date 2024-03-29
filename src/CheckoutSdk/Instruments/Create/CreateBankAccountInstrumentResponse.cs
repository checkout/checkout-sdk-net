﻿using Checkout.Common;

namespace Checkout.Instruments.Create
{
    public class CreateBankAccountInstrumentResponse : CreateInstrumentResponse
    {
        public CreateBankAccountInstrumentResponse() : base(InstrumentType.BankAccount)
        {
        }

        public BankDetails Bank { get; set; }

        public string SwiftBic { get; set; }

        public string AccountNumber { get; set; }

        public string BankCode { get; set; }

        public string Iban { get; set; }
    }
}
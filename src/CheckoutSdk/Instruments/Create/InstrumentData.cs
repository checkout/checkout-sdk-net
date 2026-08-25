using Checkout.Common;
using Checkout.Payments;
using System;

namespace Checkout.Instruments.Create
{
    public class InstrumentData
    {
        /// <summary>
        /// The type of SEPA mandate. Core or B2B.
        /// </summary>
        public SepaMandateType? Type { get; set; }

        public string AccountNumber { get; set; }

        public CountryCode? Country { get; set; }

        public Currency? Currency { get; set; }

        public PaymentType? PaymentType { get; set; }

        public string MandateId { get; set; }

        public DateTime? DateOfSignature { get; set; }
    }
}
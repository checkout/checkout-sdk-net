﻿namespace Checkout.Common.Four
{
    public class AccountHolderIdentification
    {
        public AccountHolderIdentificationType? Type { get; set; }

        public string Number { get; set; }

        public CountryCode? IssuingCountry { get; set; }
    }
}
using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    public class BankAccountField
    {
        public string Id { get; set; }

        public string Section { get; set; }

        public string Display { get; set; }

        public string HelpText { get; set; }

        public string Type { get; set; }

        public bool? Required { get; set; }

        public string ValidationRegex { get; set; }

        public int? MinLength { get; set; }

        public int? Maxlength { get; set; }

        public IList<BankAccountFieldAllowedOption> AllowedOptions { get; set; }

        public IList<BankAccountFieldDependency> Dependencies { get; set; }
    }
}
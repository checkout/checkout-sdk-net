using System.Collections.Generic;

namespace Checkout.Instruments.Get
{
    /// <summary>
    /// A single bank account field and its formatting requirements.
    /// </summary>
    public class BankAccountField
    {
        /// <summary>
        /// The field identifier.
        /// [Required]
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The section to display the field in.
        /// [Optional]
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// The field's display name.
        /// [Required]
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// The help text that explains the purpose of the field.
        /// [Optional]
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// The type of field.
        /// [Required]
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Whether the field is required.
        /// [Required]
        /// </summary>
        public bool? Required { get; set; }

        /// <summary>
        /// A regular expression that can be used to validate the input of the field.
        /// [Optional]
        /// </summary>
        public string ValidationRegex { get; set; }

        /// <summary>
        /// The minimum length of the field.
        /// [Optional]
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// The maximum length of the field.
        /// [Optional]
        /// Not aligned with the API specification: the specification declares this field as
        /// max_length, but this property serializes and deserializes as maxlength, so the API
        /// response never populates it.
        /// </summary>
        public int? Maxlength { get; set; }

        /// <summary>
        /// The allowed options for the field.
        /// [Optional]
        /// </summary>
        public IList<BankAccountFieldAllowedOption> AllowedOptions { get; set; }

        /// <summary>
        /// The field's dependencies.
        /// [Optional]
        /// </summary>
        public IList<BankAccountFieldDependency> Dependencies { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CheckoutSdk.SampleApp.Models
{
    public class PaymentModel
    {
        public const string CurrenciesViewData = "Currencies";
        public static string PublicKeyViewData = "PublicKey";

        [Required]
        public int? Amount { get; set; }
        [Required]
        public string Currency { get; set; }

        public bool DoThreeDs { get; set; }
        public string CardToken { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Checkout.SampleApp.Models
{
    public class PaymentModel
    {
        [Required]
        [Display(Name = "Amount (in minor currency unit)")]
        public int? Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Display(Name = "3-D Secure")]
        public bool DoThreeDS { get; set; }
        public string CardToken { get; set; }
        public SelectListItem[] Currencies { get; set; }
        public string PublicKey { get; set; }
    }
}

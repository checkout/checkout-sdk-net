using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Checkout.SampleApp.Models
{
    public class PaymentModel
    {
        public PaymentModel()
        {
            Capture = true;
        }
        
        [Required]
        [Display(Name = "Amount (in minor currency unit)")]
        public long? Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Display(Name = "3-D Secure")]
        public bool DoThreeDS { get; set; }
        public string CardToken { get; set; }
        public SelectListItem[] Currencies { get; set; }
        public bool Capture { get; set; }
        public string Reference { get; set; }
    }
}

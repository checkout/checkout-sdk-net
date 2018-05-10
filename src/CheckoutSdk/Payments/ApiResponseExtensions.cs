using System.Net;
using Checkout.Payments;

namespace Checkout.Payments
{
    public static class ApiResponseExtensions
    {
        public static bool RequiresRedirect(this ApiResponse apiResponse)
        {
            return apiResponse.StatusCode == HttpStatusCode.Accepted; // && check links etc.
        }

        public static CardSource GetCardSource(this PaymentSource source)
        {
            if (source.Type == "card")
                return new CardSource("4242424242424242", 12, 2012);
            return null; 
        }

        public static GiroPaySource GetGiroPaySource(this PaymentSource source)
        {
            if (source.Type == "giropay")
                return new GiroPaySource();
            return null; 
        }
    }
}
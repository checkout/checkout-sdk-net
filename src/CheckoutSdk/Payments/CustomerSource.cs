using System;

namespace Checkout.Sdk.Payments
{
    public class CustomerSource : IPaymentSource
    {
        public CustomerSource(string id, string email)
        {
            if (string.IsNullOrWhiteSpace(id)
                && string.IsNullOrWhiteSpace(email))
            {
                throw new CheckoutException($"Either {id} or {email} must be provided.");
            }

            if (!string.IsNullOrWhiteSpace(id))
            {
                Id = id;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                if(email?.Split('@').Length != 2)
                {
                    throw new CheckoutException($"{email} contain one @");
                }

                Email = email;
            }
        }

        public string Id { get; }
        public string Email { get; }
        public string Name { get; set; }
        public string Type => Consts.Source.Type.Customer;
    }
}

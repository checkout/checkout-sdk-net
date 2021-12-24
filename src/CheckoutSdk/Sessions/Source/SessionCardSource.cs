using Checkout.Common;

namespace Checkout.Sessions.Source
{
    public class SessionCardSource : SessionSource
    {
        public string Number { get; set; }
        public int? ExpiryMonth { get; set; }
        public int? ExpiryYear { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public SessionCardSource(string number, int? expiryMonth, int? expiryYear, string name, string email, SessionAddress billingAddress,
            Phone homePhone, Phone mobilePhone, Phone workPhone)
            : base(SessionSourceType.Card, billingAddress, homePhone, mobilePhone, workPhone)
        {
            Number = number;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            Name = name;
            Email = email;
        }

        public SessionCardSource() : base(SessionSourceType.Card)
        {
        }
    }
}
using Checkout.Common;

namespace Checkout.Sessions.Source
{
    public class RequestTokenSource : SessionSource
    {
        public string Token { get; set; }

        private RequestTokenSource(string token, SessionAddress billingAddress, Phone homePhone,
            Phone mobilePhone, Phone workPhone) : base(SessionSourceType.Token, billingAddress, homePhone, mobilePhone, workPhone)
        {
            Token = token;
        }

        public RequestTokenSource() : base(SessionSourceType.Token)
        {
        }
    }
}
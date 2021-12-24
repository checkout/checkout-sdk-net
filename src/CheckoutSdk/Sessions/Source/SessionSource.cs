using Checkout.Common;
using Newtonsoft.Json;

namespace Checkout.Sessions.Source
{
    public abstract class SessionSource
    {
        [JsonProperty(PropertyName = "type")]
        protected readonly SessionSourceType Type;

        [JsonProperty(PropertyName = "billing_address")]
        protected SessionAddress _billingAddress;

        [JsonProperty(PropertyName = "home_phone")]
        protected Phone _homePhone;

        [JsonProperty(PropertyName = "mobile_phone")]
        protected Phone _mobilePhone;

        [JsonProperty(PropertyName = "work_phone")]
        protected Phone _workPhone;

        protected SessionSource(SessionSourceType type, SessionAddress billingAddress, Phone homePhone,
            Phone mobilePhone, Phone workPhone)
        {
            Type = type;
            _billingAddress = billingAddress;
            _homePhone = homePhone;
            _mobilePhone = mobilePhone;
            _workPhone = workPhone;
        }

        protected internal SessionSource(SessionSourceType type)
        {
            Type = type;
        }
    }
}
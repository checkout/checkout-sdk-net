using System;

namespace Checkout.Sessions
{
    public class ThreeDsRequestorAuthenticationInfo
    {
        public ThreeDsReqAuthMethodType? ThreeDsReqAuthMethod { get; set; }
        
        public DateTime? ThreeDsReqAuthTimestamp { get; set; }
        
        public string ThreeDsReqAuthData { get; set; }
    }
}
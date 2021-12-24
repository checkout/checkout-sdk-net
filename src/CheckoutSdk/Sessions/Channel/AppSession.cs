using Newtonsoft.Json;
using System.Collections.Generic;

namespace Checkout.Sessions.Channel
{
    public class AppSession : ChannelData
    {        
        public string SdkAppId { get; set; }
        
        public long? SdkMaxTimeout { get; set; }

        [JsonProperty(PropertyName = "sdk_ephem_pub_key")]
        public SdkEphemeralPublicKey SdkEphemeralPublicKey { get; set; }

        public string SdkReferenceNumber { get; set; }
        
        public string SdkEncryptedData { get; set; }
        
        public string SdkTransactionId { get; set; }
        
        public SdkInterfaceType SdkInterfaceType { get; set; }
        
        public IList<UIElements> SdkUIElements { get; set; }        

        public AppSession() : base(ChannelType.App)
        {
        }
    }
}
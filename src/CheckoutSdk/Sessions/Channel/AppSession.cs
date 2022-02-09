#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace Checkout.Sessions.Channel
{
    public class AppSession : ChannelData
    {
        public string SdkAppId { get; set; }

        public long? SdkMaxTimeout { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("sdk_ephem_pub_key")]
#else
        [JsonProperty(PropertyName = "sdk_ephem_pub_key")]
#endif
        public SdkEphemeralPublicKey SdkEphemeralPublicKey { get; set; }

        public string SdkReferenceNumber { get; set; }

        public string SdkEncryptedData { get; set; }

        public string SdkTransactionId { get; set; }

        public SdkInterfaceType? SdkInterfaceType { get; set; }

#if NET5_0_OR_GREATER
        [JsonPropertyName("sdk_ui_elements")]
#else
        [JsonProperty(PropertyName = "sdk_ui_elements")]
#endif
        public IList<UIElements> SdkUIElements { get; set; }

        public AppSession() : base(ChannelType.App)
        {
        }
    }
}
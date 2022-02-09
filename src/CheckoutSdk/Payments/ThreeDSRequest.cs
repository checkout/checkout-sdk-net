using Checkout.Common;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Checkout.Payments
{
    public class ThreeDsRequest
    {
        public bool? Enabled { get; set; } = true;

#if NET5_0_OR_GREATER
        [JsonPropertyName("attempt_n3d")]
#else
        [JsonProperty(PropertyName = "attempt_n3d")]
#endif
        public bool? AttemptN3D { get; set; }

        public string Eci { get; set; }

        public string Cryptogram { get; set; }

        public string Xid { get; set; }

        public string Version { get; set; }

        public Exemption? Exemption { get; set; }

        public ChallengeIndicatorType? ChallengeIndicator { get; set; }
    }
}
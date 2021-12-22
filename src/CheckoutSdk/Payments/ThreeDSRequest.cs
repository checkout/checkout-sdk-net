using Newtonsoft.Json;

namespace Checkout.Payments
{
    public sealed class ThreeDsRequest 
    {
        public bool? Enabled { get; set; } = true;

        [JsonProperty(PropertyName = "attempt_n3d")]
        public bool? AttemptN3D { get; set; }

        public string Eci { get; set; }

        public string Cryptogram { get; set; }

        public string Xid { get; set; }

        public string Version { get; set; }

        public Exemption? Exemption { get; set; }
               
    }
}
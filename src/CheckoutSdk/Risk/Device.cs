using System;

namespace Checkout.Risk
{
    public class Device
    {
        public string Ip { get; set; }

        public Location Location { get; set; }

        public string Os { get; set; }

        public string Type { get; set; }

        public string Model { get; set; }

        public DateTime? Date { get; set; }

        public string UserAgent { get; set; }

        public string Fingerprint { get; set; }
    }
}
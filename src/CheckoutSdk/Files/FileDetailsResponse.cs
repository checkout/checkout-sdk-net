using Checkout.Common;
using System;

namespace Checkout.Files
{
    public class FileDetailsResponse : Resource
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public string Purpose { get; set; }

        public string Size { get; set; }

        public DateTime? UploadedOn { get; set; }
    }
}
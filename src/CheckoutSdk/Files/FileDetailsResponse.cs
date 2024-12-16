using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Files
{
    public class FileDetailsResponse : Resource
    {
        public string Id { get; set; }
        
        public string Status { get; set; }
        
        public IList<string> StatusReasons { get; set; }

        public string Size { get; set; }
        
        public string MimeType { get; set; }
        
        public DateTime? UploadedOn { get; set; }
        
        public string Filename { get; set; }

        public string Purpose { get; set; }
    }
}
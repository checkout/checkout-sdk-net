using Checkout.Common;
using System;
using System.Collections.Generic;

namespace Checkout.Reports
{
    public class ReportDetailsResponse : Resource
    {
        public string Id { get; set; }
        
        public DateTime? CreatedOn { get; set; }
        
        public DateTime? LastModifiedOn { get; set; }
        
        public string Type { get; set; }
        
        public string Description { get; set; }
        
        public AccountResponse Account { get; set; }
        
        public IList<string> Tags { get; set; }
        
        public DateTime? From { get; set; }
        
        public DateTime? To { get; set; }
        
        public IList<FileResponse> Files { get; set; }
        
    }
}

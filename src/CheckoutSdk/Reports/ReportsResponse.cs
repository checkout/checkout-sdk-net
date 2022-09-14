using Checkout.Common;
using System.Collections.Generic;

namespace Checkout.Reports
{
    public class ReportsResponse : Resource
    {
        public int? Count { get; set; }

        public int? Limit { get; set; }
        
        public IList<ReportDetailsResponse> Data { get; set; } 
    }
}

using System;

namespace Checkout.Reports
{
    public class ReportsQuery
    {
        public DateTime? CreatedAfter { get; set; }

        public DateTime? CreatedBefore { get; set; }

        public string EntityId { get; set; }
        
        public int? Limit { get; set; }

        public string PaginationToken { get; set; }
    }
}

using System.Collections.Generic;

namespace Checkout.Sessions
{
    public class Optimization
    {
        public bool Optimized { get; set; }

        public string Framework { get; set; }

        public IList<OptimizedProperties> OptimizedProperties { get; set; }
    }
}
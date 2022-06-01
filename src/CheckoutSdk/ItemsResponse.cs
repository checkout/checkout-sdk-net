using System.Collections.Generic;

namespace Checkout
{
    public class ItemsResponse<T> : HttpMetadata
    {
        public IList<T> Items { get; set; } = new List<T>();
    }
}
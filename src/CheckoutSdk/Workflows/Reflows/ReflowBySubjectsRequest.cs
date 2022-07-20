using System.Collections.Generic;

namespace Checkout.Workflows.Reflows
{
    public class ReflowBySubjectsRequest : ReflowRequest
    {
        public IList<string> Subjects { get; set; }
    }
}
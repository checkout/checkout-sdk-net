using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Checkout.Sources.Previous
{
    [Serializable]
    public sealed class ResponseData : Dictionary<string, object>
    {
        public ResponseData()
        {
        }

        private ResponseData(SerializationInfo info, StreamingContext context) : base(info,
            context)
        {
        }
    }
}
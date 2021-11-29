using System;
using System.Collections.Generic;

namespace Checkout
{
    public interface ISerializer
    {
        string Serialize(object payload);

        object Deserialize(string payload, Type objectType);

        IDictionary<string, object> Deserialize(string payload);
    }
}
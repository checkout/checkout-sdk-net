using System;

namespace Checkout.Sdk
{
    public interface ISerializer
    {
        string Serialize(object input);
        object Deserialize(string input, Type objectType);
    }
}
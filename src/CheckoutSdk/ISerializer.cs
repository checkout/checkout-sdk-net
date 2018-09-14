using System;

namespace Checkout
{
    public interface ISerializer
    {
        string Serialize(object input);
        object Deserialize(string input, Type objectType);
    }
}
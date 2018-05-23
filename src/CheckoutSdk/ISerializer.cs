namespace Checkout
{
    public interface ISerializer
    {
        string Serialize<T>(T input);
        T Deserialize<T>(string input);
    }
}
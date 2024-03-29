using System.IO;

namespace Checkout
{
    public abstract class JsonTestFixture
    {
        protected static string GetJsonFileContent(string file)
        {
            var filePath = Path.Combine(System.Environment.CurrentDirectory, file);
            return new StreamReader(filePath).ReadToEnd();
        }
    }
}
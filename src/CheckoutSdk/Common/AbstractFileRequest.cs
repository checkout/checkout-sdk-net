using System.Net.Mime;

namespace Checkout.Common
{
    public abstract class AbstractFileRequest
    {
        public string File { get; set; }

        public ContentType ContentType { get; set; }

        public AbstractFileRequest(string file, ContentType contentType)
        {
            File = file;
            ContentType = contentType;
        }
    }
}
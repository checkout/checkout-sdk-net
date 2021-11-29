using System;
using Checkout.Common;

namespace Checkout.Files
{
    public sealed class FileDetailsResponse : Resource, IEquatable<FileDetailsResponse>
    {
        public string Id { get; set; }

        public string Filename { get; set; }

        public string Purpose { get; set; }

        public string Size { get; set; }

        public DateTime? UploadedOn { get; set; }

        public bool Equals(FileDetailsResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Filename == other.Filename && Purpose == other.Purpose && Size == other.Size &&
                   UploadedOn.Equals(other.UploadedOn);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is FileDetailsResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Filename, Purpose, Size, UploadedOn);
        }
    }
}
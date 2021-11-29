using System.Runtime.Serialization;

namespace Checkout.Risk.PreCapture
{
    public enum PreCaptureDecision
    {
        [EnumMember(Value = "capture")] Capture,
        [EnumMember(Value = "flag")] Flag,
        [EnumMember(Value = "void")] Void,
    }
}
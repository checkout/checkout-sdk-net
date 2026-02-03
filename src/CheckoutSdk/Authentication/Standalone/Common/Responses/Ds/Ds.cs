namespace Checkout.Authentication.Standalone.Common.Responses.Ds
{
    /// <summary>
    /// ds
    /// The directory server (DS) information. Can be empty if the session is pending or communication with the DS
    /// failed
    /// </summary>
    public class Ds
    {
        /// <summary>
        /// Required if the session is deemed app-based. Registered application provider identifier (RID) that is unique
        /// to the payment system. RIDs are defined by the ISO 7816-5 standard. Used as part of the device data
        /// encryption process.
        /// [Optional]
        /// &lt;= 32
        /// </summary>
        public string DsId { get; set; }

        /// <summary>
        /// EMVCo-assigned unique identifier to track approved DS
        /// [Optional]
        /// &lt;= 32
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Universally unique transaction identifier assigned by the DS
        /// [Optional]
        /// 36 characters
        /// </summary>
        public string TransactionId { get; set; }
    }
}
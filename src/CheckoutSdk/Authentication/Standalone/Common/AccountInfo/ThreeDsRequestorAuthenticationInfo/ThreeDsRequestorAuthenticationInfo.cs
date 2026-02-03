using System;

namespace Checkout.Authentication.Standalone.Common.AccountInfo.ThreeDsRequestorAuthenticationInfo
{
    /// <summary>
    /// three_ds_requestor_authentication_info
    /// Information about how the 3DS Requestor authenticated the cardholder before or during the transaction.
    /// </summary>
    public class ThreeDsRequestorAuthenticationInfo
    {
        /// <summary>
        /// The mechanism used by the cardholder to authenticate with the 3DS Requestor.
        /// [Optional]
        /// </summary>
        public ThreeDsReqAuthMethodType? ThreeDsReqAuthMethod { get; set; }

        /// <summary>
        /// The UTC date and time the cardholder authenticated with the 3DS Requestor, in ISO 8601 format.
        /// [Optional]
        /// <date-time>
        /// </summary>
        public DateTime? ThreeDsReqAuthTimestamp { get; set; }

        /// <summary>
        /// Data that documents and supports a specific authentication process.
        /// [Optional]
        /// &lt;= 20000
        /// </summary>
        public string ThreeDsReqAuthData { get; set; }
    }
}
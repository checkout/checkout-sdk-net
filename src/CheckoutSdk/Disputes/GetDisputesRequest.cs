using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Checkout.Disputes
{
    /// <summary>
    /// Defines a request for a get disputes request.
    /// </summary>
    public class GetDisputesRequest
    {
        /// <summary>
        /// Creates a new get disputes request.
        /// </summary>
        /// <param name="limit">The numbers of results to return. 1 .. 250, default is 50.</param>
        /// <param name="skip">The number of results to skip, default is 0.</param>
        /// <param name="from">The ISO-8601 date and time from which to filter disputes, based on the dispute's last_update field.</param>
        /// <param name="to">The ISO-8601 date and time until which to filter disputes, based on the dispute's last_update field.</param>
        /// <param name="id">The unique identifier of the dispute.</param>
        /// <param name="statuses">One or more comma-separated statuses. This works like a logical OR operator.</param>
        /// <param name="paymentId">The unique identifier of the payment.</param>
        /// <param name="paymentReference">An optional reference (such as an order ID) that you can use later to identify the payment.</param>
        /// <param name="paymentArn">The acquirer reference number (ARN) that you can use to query the issuing bank.</param>
        public GetDisputesRequest(
            int? limit = null,
            int? skip = null,
            string from = "",
            string to = "",
            string id = "",
            string statuses = "",
            string paymentId = "",
            string paymentReference = "",
            string paymentArn = "",
            bool? thisChannelOnly = null
            )
        {
            Limit = limit;
            Skip = skip;
            From = from;
            To = to;
            Id = id;
            Statuses = statuses;
            PaymentId = paymentId;
            PaymentReference = paymentReference;
            PaymentArn = paymentArn;
            ThisChannelOnly = thisChannelOnly;
        }

        /// <summary>
        /// Gets or sets the numbers of results to return. 1 .. 250, default is 50.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets or sets the number of results to skip, default is 0.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time from which to filter disputes, based on the dispute's last_update field.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the ISO-8601 date and time until which to filter disputes, based on the dispute's last_update field.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the dispute.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets one or more comma-separated statuses.
        /// This works like a logical OR operator.
        /// </summary>
        public string Statuses { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the payment.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets an optional reference (such as an order ID) that you can use later to identify the payment.
        /// </summary>
        public string PaymentReference { get; set; }

        /// <summary>
        /// Gets or sets the acquirer reference number (ARN) that you can use to query the issuing bank.
        /// </summary>
        public string PaymentArn { get; set; }

        /// <summary>
        /// Gets or sets a boolean that, if true, only returns disputes of the specific channel that the secret key is associated with.
        /// Otherwise, returns all disputes for that business.
        /// </summary>
        public bool? ThisChannelOnly { get; set; }

        /// <summary>
        /// Gets the query path of the GetDisputesRequest.
        /// </summary>
        public string PathWithQuery(string path) {

            var query = new StringBuilder("");
            var queryParameters = new Dictionary<string, string>();
            if (Limit.HasValue) queryParameters.Add("limit", Limit.ToString());
            if (Skip.HasValue) queryParameters.Add("skip", Skip.ToString());
            if (!string.IsNullOrEmpty(From)) queryParameters.Add("from", From);
            if (!string.IsNullOrEmpty(To)) queryParameters.Add("to", To);
            if (!string.IsNullOrEmpty(Id)) queryParameters.Add("id", Id);
            if (!string.IsNullOrEmpty(Statuses)) queryParameters.Add("statuses", Statuses.Trim(' '));
            if (!string.IsNullOrEmpty(PaymentId)) queryParameters.Add("payment_id", PaymentId);
            if (!string.IsNullOrEmpty(PaymentReference)) queryParameters.Add("payment_reference", PaymentReference);
            if (!string.IsNullOrEmpty(PaymentArn)) queryParameters.Add("payment_arn", PaymentArn);
            if (ThisChannelOnly.HasValue) queryParameters.Add("this_channel_only", ThisChannelOnly.ToString());

            foreach(KeyValuePair<string, string> keyValuePair in queryParameters)
            {
                if (!query.Length.Equals(0)) query.Append("&");
                query.Append(string.Format("{0}={1}", keyValuePair.Key, WebUtility.UrlEncode(keyValuePair.Value)));
            }

            if (!query.Length.Equals(0)) query.Insert(0, "?");

            return string.Format("{0}{1}", path.Trim('/'), query);
        }
    }
}

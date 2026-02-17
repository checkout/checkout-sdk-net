using Checkout.Accounts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<TResult> Get<TResult>(
            string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default)
            where TResult : HttpMetadata;

        Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
            where TResult : HttpMetadata;

        Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            IDictionary<int, Type> resultTypeMappings,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null) 
            where TResult : HttpMetadata;

        Task<TResult> Patch<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null)
            where TResult : HttpMetadata;

        Task<TResult> Put<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null,
            Headers headers = null)
            where TResult : HttpMetadata;

        Task<TResult> Delete<TResult>(
            string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default)
            where TResult : HttpMetadata;

        Task<TResult> Query<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default)
            where TResult : HttpMetadata;
    }
}
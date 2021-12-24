using Checkout.Common;
using Checkout.Sessions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<TResult> Get<TResult>(
            string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default);

        Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null);

        Task<TResult> Post<TResult>(
            string path,
            SdkAuthorization authorization,
            IDictionary<int, Type> resultTypeMappings,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null) where TResult : Resource;

        Task<TResult> Patch<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null);

        Task<TResult> Put<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default,
            string idempotencyKey = null);

        Task<TResult> Delete<TResult>(
            string path,
            SdkAuthorization authorization,
            CancellationToken cancellationToken = default);

        Task<TResult> Query<TResult>(
            string path,
            SdkAuthorization authorization,
            object request = null,
            CancellationToken cancellationToken = default);
    }
}
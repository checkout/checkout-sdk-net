using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Checkout
{
    public interface IApiClient
    {
        Task<ApiResponse<TResult>> PostAsync<TRequest, TResult>(string path, TRequest request);
    }
}
using System.Net.Http;
using System.Threading.Tasks;

namespace Gymby.Identity.Interfaces
{
    public interface IInternalHttpClientService
    {
        Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest content);
    }
}

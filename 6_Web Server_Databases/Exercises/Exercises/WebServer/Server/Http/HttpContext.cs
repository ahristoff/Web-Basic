
namespace WebServer.Server.Http
{
    using Common;
    using Http.Contracts;

    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(IHttpRequest requestString)
        {
            CoreValidator.ThrowIfNull(requestString, nameof(requestString));

            request = requestString;
        }

        public IHttpRequest Request => this.request;
    }
}

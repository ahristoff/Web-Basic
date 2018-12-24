using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Enums;

namespace WebServer.Server.Http.Contracts
{
    using Enums;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IHttpHeaderCollection Headers { get; }
    }
}

﻿using System.Collections.Generic;
using WebServer.Server.Enums;

namespace WebServer.Server.Routing.Contracts
{
    using Enums;
    using System.Collections.Generic;

    public interface IServerRouteConfig
    {
        IDictionary<HttpRequestMethod, IDictionary<string, IRoutingContext>> Routes { get; }
    }
}

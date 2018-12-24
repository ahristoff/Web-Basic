using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Routing;

namespace WebServer.Server.Contracts
{
    using Routing.Contracts;

    public interface IApplication
    {
        void Start(IAppRouteConfig appRouteConfig);
    }
}

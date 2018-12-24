using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Application.Controllers;
using WebServer.Server.Contracts;
using WebServer.Server.Enums;
using WebServer.Server.Handlers;
using WebServer.Server.Routing;

namespace WebServer.Application
{
    using WebServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get("/", req => new HomeController().Index());

            appRouteConfig.Get("/users/{(?<name>[a-z]+)}", req => new HomeController().Index());
        }      
    }
}

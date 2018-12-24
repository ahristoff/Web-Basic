using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Application.Controllers;
using WebServer.Server.Contracts;
using WebServer.Server.Enums;
using WebServer.Server.Handlers;
using WebServer.Server.Routing;
using WebServer.Server.Routing.Contracts;

namespace WebServer.Application
{
    public class MainApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
                "/",
                req => new HomeController().Index());

            appRouteConfig.Get(
               "/testsession",
               req => new HomeController().SessionTest(req));

            appRouteConfig.Get(
                "/users/{(?<name>[A-Z][a-z]+)}",
                req => new HomeController().User(req));

            appRouteConfig.Get(
               "/users/{(?<name>[A-Z][a-z]+)}/{(?<id>[0-9]+)}",
               req => new HomeController().UserWithId(req));

            appRouteConfig.Get(
            "/calculator",
            req => new HomeController().Calculator());

            appRouteConfig.Post(
            "/calculator",
            req => new HomeController().Calculator(
                req.FormData["firstNumber"],
                req.FormData["sign"],
                req.FormData["lastNumber"]));

            appRouteConfig.Get(
           "/login",
           req => new HomeController().Login());

            appRouteConfig.Post(
            "/login",
            req => new HomeController().Login(
                req.FormData["username"],
                req.FormData["password"]));
            //ili

            //appRouteConfig
            //    .AddRoute("/", HttpRequestMethod.Get, new RequestHandler(request => new HomeController().Index()));
            //appRouteConfig
            //    .AddRoute("/testsession", HttpRequestMethod.Get, new RequestHandler(request => new HomeController().SessionTest(request)));
            //appRouteConfig
            //    .AddRoute("/users/{(?<name>[A-Z][a-z]+)}", HttpRequestMethod.Get, new RequestHandler(request => new HomeController().User(request)));
        }
    }
}


namespace WebServer.Application
{
    using WebServer.Application.Controllers;
    using WebServer.Server.Contracts;
    using WebServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {

        public void Start(IAppRouteConfig appRouteConfig)
        {
           
            appRouteConfig.Get(
            "/",
            req => new HomeController().Calculator());

            appRouteConfig.Post(
            "/",
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
        }
    }
}

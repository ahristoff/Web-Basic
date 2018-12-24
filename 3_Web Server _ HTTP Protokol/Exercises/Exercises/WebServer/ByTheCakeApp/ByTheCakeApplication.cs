
namespace WebServer.ByTheCakeApp
{
    using ByTheCakeApp.Controllers;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApplication : IApplication
    {
        public void Start(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get(
               "/",
               req => new HomeController().Index());

            appRouteConfig.Get(
              "/about",
              req => new HomeController().About());

            appRouteConfig.Get(
              "/add",
              req => new CakesController().Add());

            appRouteConfig.Post(
             "/add",
             req => new CakesController().Add(
                 req.FormData["name"],
                 req.FormData["price"])
                 );

            appRouteConfig.Get(
              "/search",
              req => new CakesController().Search(req.UrlParameters));        
        }
    }
}

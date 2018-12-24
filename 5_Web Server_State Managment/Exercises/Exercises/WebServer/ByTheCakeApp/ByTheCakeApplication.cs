
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
              req => new CakesController().Search(req));

            //------------------------------------6_Lecture
            //1.1
            appRouteConfig.Get(
              "/login",
              req => new AccountController().Login());

            //1.5
            appRouteConfig.Post(
              "/login",
              req => new AccountController().Login(req));
            // 3
            appRouteConfig.Get(
             "/shopping/add/{(?<id>[0-9]+)}",
             req => new ShoppingController().AddToCart(req));
            //4
            appRouteConfig
               .Get(
                   "/cart",
                   req => new ShoppingController().ShowCart(req));
            //5
            appRouteConfig
               .Post(
                   "/shopping/finish-order",
                   req => new ShoppingController().FinishOrder(req));

            //6
            appRouteConfig
               .Post(
                   "/logout",
                   req => new AccountController().Logout(req));
        }
    }
}

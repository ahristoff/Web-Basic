
namespace WebServer.ByTheCakeApp
{
    using ByTheCakeApp.Controllers;
    using ByTheCakeApp.Data;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using WebServer.ByTheCakeApp.ViewModels.Account;
    using WebServer.ByTheCakeApp.ViewModels.Products;

    public class ByTheCakeApplication : IApplication
    {
        public void InitializeDatabase()
        {
            using (var db = new ByTheCakeDbContext())
            {
                db.Database.Migrate();
            }
        }

        public void Start(IAppRouteConfig appRouteConfig)
        {
            //7 - 1 - 6 in appRouteConfig; serverrouteconfig
            appRouteConfig.AnonymousPaths.Add("/register"); 
            appRouteConfig.AnonymousPaths.Add("/login");

            appRouteConfig.Get(
               "/",
               req => new HomeController().Index());

            appRouteConfig.Get(
              "/about",
              req => new HomeController().About());

            //5
            appRouteConfig.Get(
              "/add",
              req => new ProductsController().Add());

            //5
            appRouteConfig.Post(
             "/add",
             req => new ProductsController().Add(
                 new AddProductViewModel
                 {
                     Name = req.FormData["name"],
                     Price = decimal.Parse(req.FormData["price"]),
                     ImageUrl = req.FormData["imageUrl"]
                 }));
                     
            //6
            appRouteConfig.Get(
              "/search",
              req => new ProductsController().Search(req));

             //3
            appRouteConfig.Get(
              "/login",
              req => new AccountController().Login());

            // 3
            appRouteConfig
                .Post(
                    "/login",req => new AccountController().Login(
                        req, new LoginViewModel
                        {
                            Username = req.FormData["name"],
                            Password = req.FormData["password"]
                        }));
         
            //8
            appRouteConfig.Get(
             "/shopping/add/{(?<id>[0-9]+)}",
             req => new ShoppingController().AddToCart(req));

            //8
            appRouteConfig
               .Get(
                   "/cart",
                   req => new ShoppingController().ShowCart(req));

            //8
            appRouteConfig
               .Post(
                   "/shopping/finish-order",
                   req => new ShoppingController().FinishOrder(req));


            appRouteConfig
               .Post(
                   "/logout",
                   req => new AccountController().Logout(req));

            //-----------------------------------------------------6_Lecture
            //2
            appRouteConfig
               .Get(
                   "/register",
                   req => new AccountController().Register());
            //2
            appRouteConfig
                .Post(
                    "/register",
                    req => new AccountController().Register(
                        req,
                        new RegisterUserViewModel
                        {
                            Username = req.FormData["username"],
                            Password = req.FormData["password"],
                            ConfirmPassword = req.FormData["confirm-password"]
                        }));
            //4
            appRouteConfig
               .Get(
                   "/profile",
                   req => new AccountController().Profile(req));

            //7
            appRouteConfig
                .Get(
                    "/cakeDetails/{(?<id>[0-9]+)}",
                    req => new ProductsController()
                        .Details(int.Parse(req.UrlParameters["id"])));

            //9
            appRouteConfig.Get(
                "/orders",
                req => new ShoppingController().ListOrders(req));

            //10
            appRouteConfig
                .Get(
                    "/orderDetails/{(?<id>[0-9]+)}",
                    req => new ShoppingController()
                        .ShowOrderDetails(int.Parse(req.UrlParameters["id"])));
        }
    }
}

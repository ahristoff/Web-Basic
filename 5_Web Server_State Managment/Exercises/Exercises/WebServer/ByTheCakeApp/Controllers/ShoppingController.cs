
namespace WebServer.ByTheCakeApp.Controllers
{
    using Server.Http.Contracts;
    using System.Collections.Generic;
    using ByTheCakeApp.Models;
    using System.IO;
    using System.Linq;
    using System;
    using Server.Http.Response;
    using ByTheCakeApp.Infrastructure;

    public class ShoppingController : Controller
    {
        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);
            var cake = this.Find(id);

            if (cake == null)
            {
                return new NotFoundResponse();
            }

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Orders.Add(cake);

            var redirectUrl = "/search";

            if (req.UrlParameters.ContainsKey("searchTerm"))
            {
                redirectUrl = $"{redirectUrl}?{"searchTerm"}={req.UrlParameters["searchTerm"]}";
            }

            return new RedirectResponse(redirectUrl);
        }

        public IHttpResponse ShowCart(IHttpRequest req)
        {
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (!shoppingCart.Orders.Any())
            {
                this.ViewData["cartItems"] = "No items in your cart";
                this.ViewData["totalCost"] = "0.00";
            }
            else
            {
                var items = shoppingCart
                    .Orders
                    .Select(i => $"<div>{i.Name} - ${i.Price:F2}</div><br />");

                var totalPrice = shoppingCart
                    .Orders
                    .Sum(i => i.Price);

                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"shopping\cart");
        }

        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();
            //clear ShoppingCart.Orders, Session is up to date          

            //SessionStore.CurrentUserKey
            return this.FileViewResponse(@"shopping\finish-order");
        }

        //-----------------------------------------------------------
        public Cake Find(int id)
        {
            var cakes = new List<Cake>();
            var savedCakes = File.ReadAllLines(@"../../../ByTheCakeApp\Data\database.csv");

            foreach (var x in savedCakes)
            {
                var currCake = x.Split(',');

                var cake = new Cake
                {
                    Id = int.Parse(currCake[0]),
                    Name = currCake[1],
                    Price = decimal.Parse(currCake[2])
                };
                cakes.Add(cake);
            }
            return cakes.FirstOrDefault(c => c.Id == id);
        }
    }
}

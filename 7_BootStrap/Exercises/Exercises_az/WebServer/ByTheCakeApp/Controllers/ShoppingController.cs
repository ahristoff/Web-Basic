
namespace WebServer.ByTheCakeApp.Controllers
{
    using Server.Http.Contracts;
    using System.Collections.Generic;
    using ByTheCakeApp.ViewModels;
    using System.IO;
    using System.Linq;
    using Server.Http.Response;
    using ByTheCakeApp.Infrastructure;
    using WebServer.ByTheCakeApp.Services;
    using WebServer.Server.Http;
    using System;
    using WebServer.ByTheCakeApp.Data.Models;
    using System.Text;

    public class ShoppingController : Controller
    {
        private readonly IUserService users;
        private readonly IProductService products;
        private readonly IShoppingService shopping;

        public ShoppingController()
        {
            this.users = new UserService();
            this.products = new ProductService();
            this.shopping = new ShoppingService();
        }

        //8
        public IHttpResponse AddToCart(IHttpRequest req)
        {
            var id = int.Parse(req.UrlParameters["id"]);

            var productExists = this.products.Exists(id);

            if (!productExists)
            {
                return new NotFoundResponse();
            }

            var product = products.FindProductById(id);

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            shoppingCart.Orders.Add(product);

            return new RedirectResponse("/search");
        }

        //8
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
                var items = shoppingCart.Orders
                    .Select(pr => $"<div>{pr.Name} - ${pr.Price:F2}</div><br />");

                var totalPrice = shoppingCart.Orders
                    .Sum(pr => pr.Price);

                this.ViewData["cartItems"] = string.Join(string.Empty, items);
                this.ViewData["totalCost"] = $"{totalPrice:F2}";
            }

            return this.FileViewResponse(@"Shopping/cart");
        }

        //8
        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            var userId = this.users.GetUserId(username);
            if (userId == null)
            {
                throw new InvalidOperationException($"User {username} does not exist");
            }

            if(!shoppingCart.Orders.Any())
            {
                return new RedirectResponse("/");
            }

            this.shopping.CreateOrder(userId.Value, shoppingCart.Orders);

            shoppingCart.Orders.Clear();

            return this.FileViewResponse(@"Shopping/finish-order");
        }

        //9
        public IHttpResponse ListOrders(IHttpRequest req)
        {
            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var orders = shopping.GetOrdersById(username);

            if (!orders.Any())
            {
                this.ViewData["results"] = "No cakes found";
            }
            else
            {
                var allOrders = orders
                    .Select(c => $@"
                      <div><a href=""/orderDetails/{c.OrderId}"">{c.OrderId}</a>  -  
                      {c.CreationDate.ToShortDateString()}  -  
                      ${c.Sum}");

                var allOrdersAsString = string.Join(Environment.NewLine, allOrders);

                this.ViewData["results"] = allOrdersAsString;
            }

            return this.FileViewResponse(@"Shopping/orders");
        }

        //10
        public IHttpResponse ShowOrderDetails(int id)
        {
            var order = shopping.FindOrder(id);

            if (order == null)
            {
                return new NotFoundResponse();
            }

            var results = new StringBuilder();

            foreach (var x in order.Products)
            {
                results.Append($@"<a href=""/cakeDetails/{x.ProductId}"">{x.Product.Name}</a> - 
                ${x.Product.Price.ToString("F2")}
                <img src=""{x.Product.ImageUrl}"" width=""40"" height=""40"" /><br />");
            }

            this.ViewData["id"] = order.OrderId.ToString();
            this.ViewData["results"] = results.ToString();
            this.ViewData["date"] = order.CreationDate.ToString();

            return this.FileViewResponse(@"Shopping/orderDetails");
        }
    }
}

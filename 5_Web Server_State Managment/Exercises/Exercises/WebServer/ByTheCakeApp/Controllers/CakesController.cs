
namespace WebServer.ByTheCakeApp.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using Models;
    using System.IO;
    using System;
    using System.Linq;

    public class CakesController : Controller
    {
        public IHttpResponse Add()// Get
        {
            this.ViewData["name"] = "";
            this.ViewData["price"] = "0";

            return this.FileViewResponse(@"Cakes/add");
        }

        public IHttpResponse Add(string name, string price)//Post
        {
            
            var streamReader = new StreamReader(@"../../../ByTheCakeApp\Data\database.csv");
            var id = streamReader.ReadToEnd().Split(Environment.NewLine).Length;
            streamReader.Dispose(); 

            using (var streamWriter = new StreamWriter(@"../../../ByTheCakeApp\Data\database.csv", true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }

            this.ViewData["name"] = name;
            this.ViewData["price"] = price;

            return this.FileViewResponse(@"Cakes/add");
        }

        public IHttpResponse Search(IHttpRequest req)
        {
            var results = string.Empty;
            this.ViewData["searchTerm"] = string.Empty;

            if (req.UrlParameters.ContainsKey("searchTerm"))
            {
                var searchTerm = req.UrlParameters["searchTerm"];

                this.ViewData["searchTerm"] = searchTerm;

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
                                    
                    if (cake.Name.Contains(searchTerm.ToLower()) && searchTerm != "")
                    {
                        var result = $@"<div>{cake.Name} - ${cake.Price:F2} 
                         <a href=""/shopping/add/{cake.Id}?searchTerm={searchTerm}"">Order</a></div>";
                        results += result;
                        results += Environment.NewLine;
                    }             
                }
                if (results == string.Empty)
                {
                    results = "Please, enter search term";
                }
            }
            
            this.ViewData["showCart"] = "none";
            this.ViewData["results"] = results;

            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);

            if (shoppingCart.Orders.Any())
            {
                var totalProducts = shoppingCart.Orders.Count;
                var totalProductsText = totalProducts != 1 ? "products" : "product";

                this.ViewData["showCart"] = "block";
                this.ViewData["products"] = $"{totalProducts} {totalProductsText}";             
            }
                     
            return this.FileViewResponse(@"Cakes/search");
        }
    }
}

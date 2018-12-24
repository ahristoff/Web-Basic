
namespace WebServer.ByTheCakeApp.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using Models;
    using System.Collections.Generic;
    using System.IO;
    using System;

    public class CakesController: Controller
    {
        private static readonly List<Cake> Cakes = new List<Cake>();

        private int n;

        public IHttpResponse Add() // Get
        {
            return this.FileViewResponse(@"Cakes/add", new Dictionary<string, string> { ["name"] = "", ["price"] = "0" });
        }

        public IHttpResponse Add(string name, string price) //Post
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            Cakes.Add(cake);

            using (var streamWriter = new StreamWriter(@"../../../ByTheCakeApp\Data\database.csv", true))
            {
                streamWriter.WriteLine($"{name},{price}");
            }

            return this.FileViewResponse(@"Cakes/add", new Dictionary<string, string> { ["name"] = name, ["price"] = price });
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            n = 0;
            var results = string.Empty;

            if (urlParameters.ContainsKey("searchTerm"))
            {
                var searchTerm = urlParameters["searchTerm"];

                var savedCakes = File.ReadAllLines(@"../../../ByTheCakeApp\Data\database.csv");
           
                foreach (var x in savedCakes)
                {                                     
                    var cake = x.Split(',');
                    var name = cake[0];
                    var price = cake[1];

                    if (name.Contains(searchTerm.ToLower()))
                    {
                        n++;

                        var result = 
                        "<form method = \"Post\" >" +
                            $"<input type = \"text\" name = \"{name}\" value = \"{name}\"/>" +
                            $"<input type = \"text\" name = \"{price}\" value = \"${price}\"/>" +
                        "</form>";

                        results += result;
                        results += Environment.NewLine;
                    }
                }
            }

            return this.FileViewResponse(@"Cakes/search", new Dictionary<string, string> { ["results"] = results, ["count"] = n.ToString() });
        }       
    }
}

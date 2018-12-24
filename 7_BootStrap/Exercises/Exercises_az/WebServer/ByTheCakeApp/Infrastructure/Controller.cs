
namespace WebServer.ByTheCakeApp.Infrastructure
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Views.Home;

    public abstract class Controller
    {      
        public Controller()
        {
            this.ViewData = new Dictionary<string, string>()
            {
                ["authDisplay"] = "block"
            };
        }

        protected IDictionary<string, string> ViewData { get; set; }
        //because of Post request Add Cake, Dictionaty save name=...,price=...

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var layoutHtml = File.ReadAllText($@"../../../ByTheCakeApp\Resources\layout.html");

            var fileHtml = File.ReadAllText($@"../../../ByTheCakeApp\Resources\{fileName}.html");

            var result = layoutHtml.Replace("{{{content}}}", fileHtml);

            if (this.ViewData.Any())
            {
                foreach (var x in ViewData)
                {
                    // escape { -> {{
                    result = result.Replace($"{{{{{{{x.Key}}}}}}}", x.Value);
                }
            }

            var response = new ViewResponse(HttpStatusCode.Ok, new FileView(result));

            return response;
        }
    }
}

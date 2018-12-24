
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

        public IHttpResponse FileViewResponse(string fileName)
        {
            var layoutHtml = File.ReadAllText($@"../../../ByTheCakeApp\Resources\layout.html");

            var fileHtml = File.ReadAllText($@"../../../ByTheCakeApp\Resources\{fileName}.html");

            var result = layoutHtml.Replace("{{{content}}}", fileHtml);

            var response = new ViewResponse(HttpStatusCode.Ok, new FileView(result));

            return response;
        }

        public IHttpResponse FileViewResponse(string fileName, Dictionary<string, string> dict)
        {
            var layoutHtml = File.ReadAllText($@"../../../ByTheCakeApp\Resources\layout.html");

            var fileHtml = File.ReadAllText($@"../../../ByTheCakeApp\Resources\{fileName}.html");

            var result = layoutHtml.Replace("{{{content}}}", fileHtml);

            if (dict != null && dict.Any())
            {
                foreach (var x in dict)
                {
                    result = result.Replace($"{{{{{{{x.Key}}}}}}}", x.Value);
                }
            }

            var response = new ViewResponse(HttpStatusCode.Ok, new FileView(result));

            return response;
        }
    }
}

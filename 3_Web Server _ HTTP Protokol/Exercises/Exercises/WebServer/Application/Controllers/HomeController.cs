
namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http.Response;
    using Server.Http.Contracts;
    using WebServer.Application.Views.Home;
    using System.IO;
    using WebServer.Application.Models;

    public class HomeController
    {
        public IHttpResponse Calculator()// Get
        {
            var result = File.ReadAllText(@"../../../Application\Resources\calculator.html");

            result = result.Replace($"{{{{{{{"results"}}}}}}}", "0"); //default value

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }

        // Post
        public IHttpResponse Calculator(string firstNumber, string sign, string lastNumber)
        {
            var result = File.ReadAllText(@"../../../Application\Resources\calculator.html");                  
            if (firstNumber == string.Empty || sign == string.Empty || lastNumber == string.Empty)
            {
                result = result.Replace($"{{{{{{{"results"}}}}}}}", "0"); //default value
                return new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));
            }

            var calc = new Calculator(firstNumber, sign, lastNumber);
            var results = calc.Calculate();

            result = result.Replace($"{{{{{{{"results"}}}}}}}", results.ToString());

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }

        public IHttpResponse Login()//Get
        {
            var result = File.ReadAllText(@"../../../Application\Resources\loginGet.html");

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }

        public IHttpResponse Login(string username, string password) //Post
        {
            var result = File.ReadAllText(@"../../../Application\Resources\loginPost.html");

            result = result.Replace($"{{{{{{{"username"}}}}}}}", username);
            result = result.Replace($"{{{{{{{"password"}}}}}}}", password);

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }
    }
}

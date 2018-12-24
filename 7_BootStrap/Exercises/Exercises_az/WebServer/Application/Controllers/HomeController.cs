
namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http;
    using Server.Http.Response;
    using Server.Http.Contracts;
    using System;
    using WebServer.Application.Views;
    using WebServer.Application.Views.Home;
    using System.IO;
    using WebServer.Application.Models;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new HomeIndexView());

            response.Cookies.Add(new HttpCookie("lang", "en")); //8 COOCIES
           

            return response;
        }

        public IHttpResponse User(IHttpRequest req)
        {
            var name = req.UrlParameters["name"];

            var response = new ViewResponse(HttpStatusCode.Ok, new UserView(name));

            return response;
        }

        public IHttpResponse UserWithId(IHttpRequest req)
        {
            var name = req.UrlParameters["name"];
            var id = int.Parse(req.UrlParameters["id"]);

            var response = new ViewResponse(HttpStatusCode.Ok, new UserViewAndId(name, id));

            return response;
        }
       
        //Get /testsesion
        public IHttpResponse SessionTest(IHttpRequest req) // 6 Session
        {
            var session = req.Session;

            const string sessionDateKey = "saved_date";

            if (session.Get(sessionDateKey) == null)
            {
                session.Add(sessionDateKey, DateTime.UtcNow);
            }

            return new ViewResponse(
                HttpStatusCode.Ok,
                new SessionTestView(session.Get<DateTime>(sessionDateKey))); //from 9 Session
                //or
                //new SessionTestView((DateTime)session.Get(sessionDateKey)));
        }

        public IHttpResponse Calculator()//html Get
        {
            var result = File.ReadAllText(@"../../../Application\Resources\calculator.html");

            result = result.Replace($"{{{"results"}}}", "0"); //default value

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }

        //html Post
        public IHttpResponse Calculator(string firstNumber, string sign, string lastNumber)
        {
            var result = File.ReadAllText(@"../../../Application\Resources\calculator.html");                  
            if (firstNumber == string.Empty || sign == string.Empty || lastNumber == string.Empty)
            {
                result = result.Replace($"{{{"results"}}}", "0"); //default value
                return new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));
            }

            var calc = new Calculator(firstNumber, sign, lastNumber);
            var results = calc.Calculate();

            result = result.Replace($"{{{"results"}}}", results.ToString());

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }

        public IHttpResponse Login()//Get
        {
            var result = File.ReadAllText(@"../../../Application\Resources\loginGet.html");

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }

        public IHttpResponse Login(string username, string password)
        {
            var result = File.ReadAllText(@"../../../Application\Resources\loginPost.html");

            result = result.Replace($"{{{"username"}}}", username);
            result = result.Replace($"{{{"password"}}}", password);

            var response = new ViewResponse(HttpStatusCode.Ok, new CalculatorView(result));

            return response;
        }
    }
}

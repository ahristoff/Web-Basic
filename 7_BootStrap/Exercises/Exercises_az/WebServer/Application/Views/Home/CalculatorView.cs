
namespace WebServer.Application.Views.Home
{
    using System;
    using WebServer.Server.Contracts;

    public class CalculatorView : IView
    {
        private readonly string htmlFile;

        public CalculatorView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View()
        {
            return this.htmlFile;
        }
    }
}

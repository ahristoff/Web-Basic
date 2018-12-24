using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    class HomeIndexView : IView
    {
        public string View()
        {
            return "<h1>Welcome<h1>";
        }
    }
}

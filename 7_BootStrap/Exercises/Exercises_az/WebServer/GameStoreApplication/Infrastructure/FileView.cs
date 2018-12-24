﻿
namespace WebServer.GameStoreApplication.Infrastructure
{
    using WebServer.Server.Contracts;

    public class FileView : IView
    {
        private readonly string htmlFile;

        public FileView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View()
        {
            return this.htmlFile;
        }
    }
}

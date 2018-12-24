
namespace SimpleMvc.Framework.Routers
{
    using System;
    using System.IO;
    using System.Linq;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class RecourceRouter : IHandleable
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            var fileName = request.Path.Split('/').Last();
            var fileExtentions = fileName.Split('.').Last();

            try
            {
                var fileContents = this.ReadFile(fileName, fileExtentions);

                return new FileResponse(HttpStatusCode.Found, fileContents);
            }
            catch
            {
                return new NotFoundResponse();
            }
        }

        private byte[] ReadFile(string fileName, string fileExtentions)
        {
            return File.ReadAllBytes(string.Format("{0}\\{1}\\{2}",
                MvcContext.Get.ResourcesFolder, fileExtentions, fileName));
        }
    }
}

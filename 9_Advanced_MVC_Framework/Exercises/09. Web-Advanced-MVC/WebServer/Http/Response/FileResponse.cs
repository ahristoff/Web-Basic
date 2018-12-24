
using WebServer.Enums;
using WebServer.Exceptions;

namespace WebServer.Http.Response
{
    public class FileResponse: HttpResponse
    {
        public FileResponse(HttpStatusCode statusCode, byte[] fileContents)
        {
            var statusCodeNumber = (int)statusCode;

            if (299 >= statusCodeNumber || statusCodeNumber >= 400)
            {
                throw new InvalidResponseException("File response need a status code between 300 and 400");
            }

            this.Filedata = fileContents;
            this.StatusCode = statusCode;

            this.Headers.Add(HttpHeader.ContentLength, fileContents.Length.ToString());
            this.Headers.Add(HttpHeader.ContentDisposition, "attachment");


        }

        public byte[] Filedata { get; private set; }
    }
}

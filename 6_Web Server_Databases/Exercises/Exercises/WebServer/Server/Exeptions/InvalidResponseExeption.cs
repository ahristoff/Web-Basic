
namespace WebServer.Server.Exeptions
{
    using System;

    class InvalidResponseExeption: Exception
    {
        public InvalidResponseExeption(string message)
            :base(message)
        {

        }
    }
}

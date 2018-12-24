
namespace _3_Request_Parser
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var validUrls = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();
                if (line =="END")
                {
                    break;
                }

                var urlParts = line.Split(new[] { "/"},StringSplitOptions.RemoveEmptyEntries);

                var path = urlParts[0];
                var method = urlParts[1].ToUpper();

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }

                validUrls[path].Add(method);
            }

            var request = Console.ReadLine().Split(new[] { " ", "/" },StringSplitOptions.RemoveEmptyEntries);
           
            var requestMethod = request[0];
            var requestUrl = request[1];
            var requestProtokol = request[2] + "/" + request[3];

            var responseStatus = 404;
            var responseStatusText = "Not Found";

            if (validUrls.ContainsKey(requestUrl) && validUrls[requestUrl].Contains(requestMethod))
            {
                //Ok
                responseStatus = 200;
                responseStatusText = "OK";
            }

            Console.WriteLine();
            Console.WriteLine($"{requestProtokol} {responseStatus} {responseStatusText}");
            Console.WriteLine($"Content-Lenght: {responseStatusText.Length}");
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine($"{responseStatusText}");
        }
    }
}


namespace _2_Validate_URL
{
    using System;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine(); ;
            var decodedUrl = WebUtility.UrlDecode(url);

            Console.WriteLine(decodedUrl);

            var parsedUrl = new Uri(url);

            if (string.IsNullOrEmpty(parsedUrl.Scheme))
            {
                Console.WriteLine("Invalid Protokol");
            }
            else
            {
                Console.WriteLine($"Protocol: {parsedUrl.Scheme}");
            }

            if (string.IsNullOrEmpty(parsedUrl.Host))
            {
                Console.WriteLine("Invalid Host");
            }
            else
            {
                Console.WriteLine($"Host: {parsedUrl.Host}");
            }

            if (parsedUrl.Port== 0)
            {
                Console.WriteLine("Invalid Port");
            }
            else
            {
                Console.WriteLine($"Port: {parsedUrl.Port}");
            }

            if (string.IsNullOrEmpty(parsedUrl.AbsolutePath))
            {
                Console.WriteLine("Invalid Path");
            }
            else
            {
                Console.WriteLine($"Path: {parsedUrl.AbsolutePath}");
            }

            if (string.IsNullOrEmpty(parsedUrl.Query))
            {
                Console.WriteLine("Invalid Query");
            }
            else
            {
                Console.WriteLine($"Query: {parsedUrl.Query.Substring(1, parsedUrl.Query.Length - 1)}");//without->?
            }

            if (string.IsNullOrEmpty(parsedUrl.Fragment))
            {
                Console.WriteLine("Invalid Fragmant");
            }
            else
            {
                Console.WriteLine($"Fragment: {parsedUrl.Fragment.Substring(1, parsedUrl.Fragment.Length - 1)}");
            }          
        }
    }
}

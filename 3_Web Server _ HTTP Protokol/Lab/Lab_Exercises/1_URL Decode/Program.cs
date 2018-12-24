
namespace _1_URL_Decode
{
    using System;
    using System.Net;

    class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            string decodedUrl = WebUtility.UrlDecode(url);
            Console.WriteLine(decodedUrl);
        }
    }
}

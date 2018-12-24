﻿
namespace _3_Simple_Web_Server
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {           
            Task.Run(async() =>
            {
                await Connect(); 
            })
           .GetAwaiter()
           .GetResult();          
        }
        public static async Task Connect()
        {
            var port = 1338;
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var tcpListener = new TcpListener(ipAddress, port);

            tcpListener.Start();

            while (true)
            {
                var client = await tcpListener.AcceptTcpClientAsync();
     
                var buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);
                var clientMessage = Encoding.UTF8.GetString(buffer);

                Console.WriteLine(clientMessage.Trim('\0'));

                var response = "HTTP/1.1 200 OK\n\nHello from my server!";
              
                var data = Encoding.UTF8.GetBytes(response);
                await client.GetStream().WriteAsync(data, 0, data.Length);

                client.Dispose();
            }
        }
    }
}

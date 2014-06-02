using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NLog.NetworkTarget
{
    class Program
    {
        static void Main(string[] args)
        {
            Listen();

            Console.ReadKey();
        }

        static async void Listen()
        {
            while (true)
            {
                try
                {
                    var port = int.Parse(ConfigurationManager.AppSettings["port"]);

                    var server = new TcpListener(IPAddress.Any, port);

                    Console.WriteLine("Listening on port {0}", port);

                    server.Start();

                    while (true)
                    {
                        using (var client = await server.AcceptTcpClientAsync())
                        {
                            Console.WriteLine("Client connected... ");

                            var buffer = new byte[256];
                            var stream = client.GetStream();
                            var size = 0;
                            var data = new StringBuilder();
                            while ((size = stream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                            }

                            var targetFile = Path.Combine(ConfigurationManager.AppSettings["destination:file:path"], string.Format(ConfigurationManager.AppSettings["destination:file:name-format"], DateTime.Now));

                            File.AppendAllText(targetFile, data.ToString() + Environment.NewLine, Encoding.UTF8);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong... " + ex.Message);
                }
            }
        }
    }
}

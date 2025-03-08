using System;
using System.Configuration;
using AppKit;
using Networking.Client;
using Services;

namespace Client
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading properties from app.config ...");
            int port;
            String portConfig = ConfigurationManager.AppSettings["port"];
            if (portConfig == null)
            {
                Console.WriteLine("Port property not set.");
                return;
            }
            else
            {
                bool result = Int32.TryParse(portConfig, out port);
                if (!result)
                {
                    Console.WriteLine("Port property not a number.");
                    return;
                }
            }

            String ip = ConfigurationManager.AppSettings["ip"];

            if (ip == null)
            {
                Console.WriteLine("Port property not set.");
                return;
            }

            Console.WriteLine("Using  server on IP {0} and port {1}", ip, port);
            SingletonProxy.Init(ip, port);
            
            NSApplication.Init();
            NSApplication.Main(args);
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            NetClient client = new NetClient();
            client.connectServer("127.0.0.1", 12354);
            while(true)
            {
                string input = "";
                input = Console.ReadLine();
                client.sendMessage(input);
            }
        }
    }
}

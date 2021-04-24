using System;
using System.Net.Sockets;
using SocketServer.Game.CenterServer;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerCenter.getInstance().start(12354);

            while (true)
            { 
                
            }
        }
    }
}

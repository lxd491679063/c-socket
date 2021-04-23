using System;
using System.Net.Sockets;
using ServerCommon;
using SocketServer.Game.CenterServer;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ServerCenter center_server = new ServerCenter();
            center_server.StartBio(12343);

            while (true)
            { 
                
            }
        }
    }
}

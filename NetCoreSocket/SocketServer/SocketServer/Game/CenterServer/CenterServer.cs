using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;

namespace SocketServer.Game.CenterServer
{
    public class ServerCenter
    {
        #region SingleInstance
        private static ServerCenter _instance = new ServerCenter();

        public static ServerCenter getInstance() { return _instance; }
        #endregion

        public static Socket server_socket { get; private set; }
        private ServerListener _server_listener = null;
        public ServerCenter()
        {
            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void start(int server_port)
        {
            if (_server_listener == null)
            {
                _server_listener = new ServerListener();
            }

            _server_listener.start(server_port);
        }
    }
}

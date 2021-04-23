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
        public Socket server_socket { get; private set; }

        public ServerCenter()
        {
            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 阻塞式的监听
        /// </summary>
        /// <param name="server_port">端口号</param>
        public void StartBio(int server_port)
        {
            int host_port = server_port;
            IPAddress host_ip = IPAddress.Any;
            IPEndPoint host_point = new IPEndPoint(host_ip, host_port);
            server_socket.Bind(host_point);
            server_socket.Listen(100);

            Thread thread = new Thread(onAcceptBio);
            thread.Start();
            //Console.ReadLine();

            Console.WriteLine("服务器启动成功,端口号为：{0}:{1}", host_point.Address.ToString(), host_point.Port.ToString());
        }

        /// <summary>
        /// 接收阻塞式的监听
        /// </summary>
        private void onAcceptBio()
        {
            while (true)
            {
                //为新的客户端连接创建一个Socket对象
                Socket clientSocket = server_socket.Accept();
                IPEndPoint receive_ip_point = clientSocket.RemoteEndPoint as IPEndPoint;
                //Console.WriteLine("客户端{0}成功连接", receive_ip_point.Address.ToString(), clientSocket.RemoteEndPoint.);
                //向连接的客户端发送连接成功的数据
                //ByteBuffer buffer = new ByteBuffer();
                //buffer.WriteString("Connected Server");
                //clientSocket.Send(WriteMessage(buffer.ToBytes()));
                //每个客户端连接创建一个线程来接受该客户端发送的消息
                Thread thread = new Thread(RecieveMessage);
                thread.Start(clientSocket);
            }
        }
        private void RecieveMessage(object clientSocket)
        {

        }
    }
}

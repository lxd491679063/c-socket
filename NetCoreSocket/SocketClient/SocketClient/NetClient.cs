using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using GameService;

namespace SocketClient
{
    class NetClient
    {
        public static Socket _client_socket = null;
        public NetClient()
        {
            if(_client_socket == null)
            {
                _client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        public bool connectServer(string ip_address, int server_port)
        {
            IPAddress mIp = IPAddress.Parse(ip_address);
            IPEndPoint ip_end_point = new IPEndPoint(mIp, server_port);
            try
            {
                _client_socket.Connect(ip_end_point);
                Console.WriteLine("连接服务器成功");

                Thread thread = new Thread(recieveMessage);
                thread.Start();

                return true;
            }
            catch
            {
                Console.WriteLine("连接服务器失败");
                return false;
            }
        }

        private void recieveMessage()
        {
            Socket recieve_socket = _client_socket;

            while (true)
            {
                try
                {
                    //定义接收数组
                    byte[] buffer = new byte[1024 * 1024];
                    int buff_len = recieve_socket.Receive(buffer);

                    ByteBuffer buffer_new = new ByteBuffer(buffer);
                    int proc_type = buffer_new.ReadInt();
                    Console.WriteLine("接收到信息:" + recieve_socket.RemoteEndPoint.ToString() + ": proc_type = " + proc_type.ToString());
                    checkProdef(ref buffer_new);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务端断开连接了！");
                }
            }
        }

        public void sendMessage(string message)
        {
            //Console.WriteLine("将要发送消息：" + message);
            //byte[] send_buffer = Encoding.UTF8.GetBytes(message);

            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInt(1);
            buffer.WriteInt(520);
            _client_socket.Send(buffer.ToBytes());
        }

        private void checkProdef(ref ByteBuffer in_buffer)
        {
            int content = in_buffer.ReadInt();
            Console.WriteLine("接收到信息: content = " + content.ToString());
        }
    }
}

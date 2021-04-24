using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using GameService;

namespace SocketServer.Game.CenterServer
{
    public class ServerListener
    {
        private Socket _Listener;
        private IPEndPoint _IP;
        private volatile bool IsInit = false;
        //private List<TSocketBase> sockets = new List<TSocketBase>();
        private List<Socket> socket_list = new List<Socket>();
        static private object socket_list_locker = new object (); 

        /// <summary>
        /// 获取绑定终结点
        /// </summary>
        public IPEndPoint IP
        {
            get { return this._IP; }
        }

        public ServerListener()
        {

        }

        public void start(int server_port)
        {
            if (IsInit)
            {
                return;
            }
            IsInit = true;
            IPEndPoint localEP = new IPEndPoint(IPAddress.Any, server_port);
            this._IP = localEP;

            try
            {
                Console.WriteLine(string.Format("Listen Tcp -> {0}:{1} ", localEP.Address, localEP.Port));
                this._Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._Listener.Bind(this._IP);
                this._Listener.Listen(5000);

                Thread accept_thread = new Thread(onAcceptBio);
                accept_thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                this.Dispose();
            }
        }

        /// <summary>
        /// 接收阻塞式的监听
        /// </summary>
        private void onAcceptBio()
        {
            while (true)
            {
                //为新的客户端连接创建一个Socket对象
                Socket clientSocket = _Listener.Accept();
                socket_list.Add(clientSocket);
                IPEndPoint receive_ip_point = clientSocket.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("客户端{0}:{1}成功连接", receive_ip_point.Address.ToString(), receive_ip_point.Port);
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
            Socket recieve_socket = clientSocket as Socket;

            while(true)
            {
                try
                {
                    //定义接收数组
                    byte[] buffer = new byte[1024 * 1024];
                    int buff_len = recieve_socket.Receive(buffer);

                    //string words = Encoding.UTF8.GetString(buffer, 0, buff_len);
                    ByteBuffer buffer_new = new ByteBuffer(buffer);
                    int proc_type = buffer_new.ReadInt();
                    ByteBuffer out_buffer = new ByteBuffer();
                    checkProdef(proc_type,ref buffer_new,ref out_buffer);


                    //string send_message = "服务端返回消息：" + words;
                    //byte[] send_buffer = Encoding.UTF8.GetBytes(send_message);
                    recieve_socket.Send(out_buffer.ToBytes());
                }
                catch(Exception ex)
                {
                    this.removeSocket(recieve_socket);
                }
            }
        }

        private void checkProdef(int proc_type,ref ByteBuffer indata,ref ByteBuffer reply)
        {
            switch(proc_type)
            {
                case 1:
                    {
                        int content = indata.ReadInt();
                        Console.WriteLine("接收到的消息 proc_type = {0},content={1}", proc_type, content);

                        reply.WriteInt(proc_type);
                        reply.WriteInt(content);
                        //Console.WriteLine("接收到信息" + recieve_socket.RemoteEndPoint.ToString() + ":" + words);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Error PROC_TYPE!");
                        break;
                    }
            }
        }

        private void removeSocket(Socket target_socket)
        {
            lock (socket_list_locker)
            {
                socket_list.Remove(target_socket);
            }
        }
        public void startAsync(int server_port)
        { 
        
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (IsInit)
            {
                IsInit = false;
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// 释放所占用的资源
        /// </summary>
        /// <param name="flag1"></param>
        protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool flag1)
        {
            if (flag1)
            {
                if (_Listener != null)
                {
                    try
                    {
                        Console.WriteLine(string.Format("Stop Listener Tcp -> {0}:{1} ", this.IP.Address.ToString(),
                            this.IP.Port));
                        _Listener.Close();
                        _Listener.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}

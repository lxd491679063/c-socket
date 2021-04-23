using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace SocketServer.Game.CenterServer
{
    public class TCPListener
    {
        //private IPEndPoint _IP;
        //private Socket _Listeners;
        //private volatile bool IsInit = false;
        //private List<TSocketBase> sockets = new List<TSocketBase>();

        ///// <summary>
        ///// 初始化服务器
        ///// </summary>
        //public TCPListener(string ip = "0.0.0.0", int port = 9527)
        //{
        //    IsInit = true;
        //    IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ip), port);
        //    this._IP = localEP;
        //    try
        //    {
        //        Console.WriteLine(string.Format("Listen Tcp -> {0}:{1} ", ip, port));
        //        this._Listeners = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //        this._Listeners.Bind(this._IP);
        //        this._Listeners.Listen(5000);
        //        SocketAsyncEventArgs sea = new SocketAsyncEventArgs();
        //        sea.Completed += new EventHandler<SocketAsyncEventArgs>(this.AcceptAsync_Async);
        //        this.AcceptAsync(sea);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        this.Dispose();
        //    }
        //}

        //private void AcceptAsync(SocketAsyncEventArgs sae)
        //{
        //    if (IsInit)
        //    {
        //        if (!this._Listeners.AcceptAsync(sae))
        //        {
        //            AcceptAsync_Async(this, sae);
        //        }
        //    }
        //    else
        //    {
        //        if (sae != null)
        //        {
        //            sae.Dispose();
        //        }
        //    }
        //}

        //private void AcceptAsync_Async(object sender, SocketAsyncEventArgs sae)
        //{
        //    if (sae.SocketError == SocketError.Success)
        //    {
        //        var socket = new TSocketClient(sae.AcceptSocket);
        //        sockets.Add(socket);
        //        Console.WriteLine("Remote Socket LocalEndPoint：" + sae.AcceptSocket.LocalEndPoint + " RemoteEndPoint：" +
        //                          sae.AcceptSocket.RemoteEndPoint.ToString());
        //    }
        //    sae.AcceptSocket = null;
        //    if (IsInit)
        //    {
        //        this._Listeners.AcceptAsync(sae);
        //    }
        //    else
        //    {
        //        sae.Dispose();
        //    }
        //}

        ///// <summary>
        ///// 释放资源
        ///// </summary>
        //public void Dispose()
        //{
        //    if (IsInit)
        //    {
        //        IsInit = false;
        //        this.Dispose(true);
        //        GC.SuppressFinalize(this);
        //    }
        //}

        ///// <summary>
        ///// 释放所占用的资源
        ///// </summary>
        ///// <param name="flag1"></param>
        //protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool flag1)
        //{
        //    if (flag1)
        //    {
        //        if (_Listeners != null)
        //        {
        //            try
        //            {
        //                Console.WriteLine(string.Format("Stop Listener Tcp -> {0}:{1} ", this.IP.Address.ToString(),
        //                    this.IP.Port));
        //                _Listeners.Close();
        //                _Listeners.Dispose();
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 获取绑定终结点
        ///// </summary>
        //public IPEndPoint IP
        //{
        //    get { return this._IP; }
        //}
    }
}

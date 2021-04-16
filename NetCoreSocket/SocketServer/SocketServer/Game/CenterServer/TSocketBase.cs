using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SocketServer.Game.CenterServer
{
    public abstract class TSocketBase
    {
        //封装socket
        internal Socket _Socket;
        //回调
        private AsyncCallback aCallback;
        //接受数据的缓冲区
        private byte[] Buffers;
        //标识是否已经释放
        private volatile bool IsDispose;
        //10K的缓冲区空间
        private int BufferSize = 10 * 1024;
        //收取消息状态码
        private SocketError ReceiveError;
        //发送消息的状态码
        private SocketError SenderError;
        //每一次接受到的字节数
        private int ReceiveSize = 0;
        //接受空消息次数
        private byte ZeroCount = 0;

        public abstract void Receive(byte[] rbuff);

        public void SetSocket()
        {
            this.aCallback = new AsyncCallback(this.ReceiveCallback);
            this.IsDispose = false;
            this._Socket.ReceiveBufferSize = this.BufferSize;
            this._Socket.SendBufferSize = this.BufferSize;
            this.Buffers = new byte[this.BufferSize];
        }


        /// <summary>
        /// 关闭并释放资源
        /// </summary>
        /// <param name="msg"></param>
        public void Close(string msg)
        {
            if (!this.IsDispose)
            {
                this.IsDispose = true;
                try
                {
                    try
                    {
                        this._Socket.Close();
                    }
                    catch
                    {
                    }
                    IDisposable disposable = this._Socket;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                    this.Buffers = null;
                    GC.SuppressFinalize(this);
                }
                catch (Exception)
                {
                }
            }
        }


        /// <summary>
        /// 递归接收消息方法
        /// </summary>
        internal void ReceiveAsync()
        {
            try
            {
                if (!this.IsDispose && this._Socket.Connected)
                {
                    this._Socket.BeginReceive(this.Buffers, 0, this.BufferSize, SocketFlags.None, out SenderError,
                        this.aCallback, this);
                    CheckSocketError(ReceiveError);
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                this.Close("链接已经被关闭");
            }
            catch (System.ObjectDisposedException)
            {
                this.Close("链接已经被关闭");
            }
        }



        /// <summary>
        /// 接收消息回调函数
        /// </summary>
        /// <param name="iar"></param>
        private void ReceiveCallback(IAsyncResult iar)
        {
            if (!this.IsDispose)
            {
                try
                {
                    //接受消息
                    ReceiveSize = _Socket.EndReceive(iar, out ReceiveError);
                    //检查状态码
                    if (!CheckSocketError(ReceiveError) && SocketError.Success == ReceiveError)
                    {
                        //判断接受的字节数
                        if (ReceiveSize > 0)
                        {
                            byte[] rbuff = new byte[ReceiveSize];
                            Array.Copy(this.Buffers, rbuff, ReceiveSize);
                            this.Receive(rbuff);
                            //重置连续收到空字节数
                            ZeroCount = 0;
                            //继续开始异步接受消息
                            ReceiveAsync();
                        }
                        else
                        {
                            ZeroCount++;
                            if (ZeroCount == 5)
                            {
                                this.Close("错误链接");
                            }
                        }
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    this.Close("链接已经被关闭");
                }
                catch (System.ObjectDisposedException)
                {
                    this.Close("链接已经被关闭");
                }
            }
        }

        /// <summary>
        /// 错误判断
        /// </summary>
        /// <param name="socketError"></param>
        /// <returns></returns>
        private bool CheckSocketError(SocketError socketError)
        {
            switch ((socketError))
            {
                case SocketError.SocketError:
                case SocketError.VersionNotSupported:
                case SocketError.TryAgain:
                case SocketError.ProtocolFamilyNotSupported:
                case SocketError.ConnectionAborted:
                case SocketError.ConnectionRefused:
                case SocketError.ConnectionReset:
                case SocketError.Disconnecting:
                case SocketError.HostDown:
                case SocketError.HostNotFound:
                case SocketError.HostUnreachable:
                case SocketError.NetworkDown:
                case SocketError.NetworkReset:
                case SocketError.NetworkUnreachable:
                case SocketError.NoData:
                case SocketError.OperationAborted:
                case SocketError.Shutdown:
                case SocketError.SystemNotReady:
                case SocketError.TooManyOpenSockets:
                    this.Close(socketError.ToString());
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 发送消息方法
        /// </summary>
        internal int SendMsg(byte[] buffer)
        {
            int size = 0;
            try
            {
                if (!this.IsDispose)
                {
                    size = this._Socket.Send(buffer, 0, buffer.Length, SocketFlags.None, out SenderError);
                    CheckSocketError(SenderError);
                }
            }
            catch (System.ObjectDisposedException)
            {
                this.Close("链接已经被关闭");
            }
            catch (System.Net.Sockets.SocketException)
            {
                this.Close("链接已经被关闭");
            }
            buffer = null;
            return size;
        }
    }
}

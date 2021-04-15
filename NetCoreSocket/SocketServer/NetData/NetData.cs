using System;
using System.IO;
using System.Text;

namespace NetData
{
    public class NetDataReader
    {
        #region Properties

        /// <summary>
        /// m_stream 数据流
        /// </summary>
        MemoryStream m_stream = null;

        /// <summary>
        /// m_reader 二进制数据读取
        /// </summary>
        BinaryReader m_reader = null;

        /// <summary>
        /// m_dataLength 数据长度
        /// </summary>
        ushort m_dataLength;

        #endregion

        #region Ctors
        public NetDataReader(byte[] data){
            if (data != null)
            {
                m_stream = new MemoryStream(data);
                m_reader = new BinaryReader(m_stream);

                //m_dataLength = ReadUShort();
            }
        }
        #endregion
    }
}

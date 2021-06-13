using System;
using System.Net;
using System.Net.Sockets;

namespace cantact_new
{
    /// <summary>
    /// 원격 제어 이벤트를 수신하는 서버
    /// </summary>
    public class RecvEventServer
    {
        Socket lis_sock;
        /// <summary>
        /// 이벤트를 수신여부를 알리는 이벤트
        /// </summary>
        public event RecvKMEEventHandler RecvedKMEvent = null;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">포트</param>
        public RecvEventServer(string ip, int port)
        {
            // 다른 리시브 서버 주석이랑 동일함
            lis_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            lis_sock.Bind(ep);
            lis_sock.Listen(5);
            lis_sock.BeginAccept(DoAccept, null);
        }

        void DoAccept(IAsyncResult result)
        {
            if(lis_sock != null)
            {
                Socket dosock = lis_sock.EndAccept(result);
                lis_sock.BeginAccept(DoAccept, null);
                Receive(dosock);
            }
        }

        private void Receive(Socket dosock)
        {
            byte[] buffer = new byte[9];
            int n = dosock.Receive(buffer);
            if(RecvedKMEvent != null)
            {
                RecvKMEEventArgs e = new RecvKMEEventArgs(new Meta(buffer));
                RecvedKMEvent(this, e);
            }
            dosock.Close();
        }

        public void Close()
        {
            if(lis_sock != null)
            {
                lis_sock.Close();
                lis_sock = null;
            }
        }

    }
}

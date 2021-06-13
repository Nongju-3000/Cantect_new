using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;

namespace cantact_new
{/// <summary>
/// 원격 제어 요청 클라이언트
/// </summary>
    public static class SetupClient
    {
        public static event EventHandler ConnectedEventHandler = null;
        public static event EventHandler ConnectFailedEventHandler = null;
        static Socket sock;

        /// <summary>
        /// 원격 제어 요청 관련 메소드
        /// </summary>
        /// <param name="ip">요청할 상대의 IP 주소</param>
        /// <param name="port">요청할 상대의 포트 번호</param>
        public static void Setup(string ip, int port)
        {
            IPAddress ipaddr = IPAddress.Parse(ip); 
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            //TCP소켓 생성(네트워크 체계, 전송방식, TCP프로토콜) 
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.BeginConnect(ep, DoConnect, null);
        }

        /// <summary>
        /// 연결 결과 메소드
        /// </summary>
        /// <param name="result">연결 결과</param>
        static void DoConnect(IAsyncResult result)
        {
            AsyncResult ar = result as AsyncResult;
            try
            {
                sock.EndConnect(result);
                if(ConnectedEventHandler != null)
                {
                    ConnectedEventHandler(null, new EventArgs()); // 연결성공
                }
            }

            catch
            {
                if(ConnectFailedEventHandler != null)
                {
                    ConnectFailedEventHandler(null, new EventArgs()); // 연결실패
                }
            }
            sock.Close();
        }
    }
}

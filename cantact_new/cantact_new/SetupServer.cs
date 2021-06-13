// 원격 제어 요청을 수신하는 클래스
using RCEventArgsLib;
using System;
using System.Net;
using System.Net.Sockets;

namespace cantact_new
{/// <summary>
/// 원격 제어 요청을 수신하는 서버 클래스
/// </summary>
    public static class SetupServer
    {
        static Socket lis_sock; // 연결 요청을 수신하는 소켓
        /// <summary>
        /// 연결 요청을 수신하는 이벤트 핸들러
        /// </summary>
        static public event RecvRCInfoEventHandler RecvedRCInfoEventHandler = null;
        static string ip;
        static int port;

        /// <summary>
        /// 수신 서버 시작
        /// </summary>
        /// <param name="ip">IP주소</param>
        /// <param name="port">포트</param>
        public static void Start(string ip, int port)
        {
            SetupServer.ip = ip;
            SetupServer.port = port;
            SocketBooting();
        }
        /// <summary>
        /// IPEndPoint와 소켓 생성하는 함수
        /// </summary>
        private static void SocketBooting()
        {
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            lis_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            lis_sock.Bind(ep);  // 생성된 소켓과 IPEndPoint를 합침
            lis_sock.Listen(1); // Back 로그 큐
            lis_sock.BeginAccept(DoAccept, null); // 연결 허용을 비동기식으로 처리함
        }

        /// <summary>
        /// 연결 허용 메소드
        /// </summary>
        /// <param name="result">연결결과</param>
        static void DoAccept(IAsyncResult result)
        {
            if(lis_sock == null)
            {
                return; // 실패
            }
            try
            {
                Socket sock = lis_sock.EndAccept(result);   // 성공
                DoIt(sock);
                lis_sock.BeginAccept(DoAccept, null);
            }
            catch
            {
                Close(); // 예외 발생할경우 소켓 초기화
            }
        }
        
        /// <summary>
        /// 원격 제어 요청 메소드
        /// </summary>
        /// <param name="dosock"></param>
        static void DoIt(Socket dosock)
        {
            if(RecvedRCInfoEventHandler != null)
            {
                RecvRCInfoEventArgs e = new RecvRCInfoEventArgs(dosock.RemoteEndPoint);
                RecvedRCInfoEventHandler(null, e);
            }
            dosock.Close();
        }

        /// <summary>
        /// 소켓 닫는 메소드
        /// </summary>
        public static void Close()
        {
            if(lis_sock != null)
            {
                lis_sock.Close();
                lis_sock = null;
            }
        }
    }
}

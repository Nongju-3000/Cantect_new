using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace cantact_new
{
    /// <summary>
    /// 이미지 수신 서버
    /// </summary>
    public class ImageServer
    {
        Socket lis_sock;
        /// <summary>
        /// 이미지 수신 이벤트
        /// </summary>
        public event RecvImageEventHandler RecvedImage = null;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip">IP주소</param>
        /// <param name="port">포트</param>
        public ImageServer(string ip, int port)
        {
            lis_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            lis_sock.Bind(ep);
            lis_sock.Listen(5); // Back 로그 큐 크기
            lis_sock.BeginAccept(DoAccept, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        void DoAccept(IAsyncResult result)
        {
            if(lis_sock == null)
            {
                return;
            }
            try 
            {
                Socket dosock = lis_sock.EndAccept(result);
                Recevice(dosock);
                lis_sock.BeginAccept(DoAccept, null);
            }

            catch
            {
                Close();
            }
        }

        /// <summary>
        /// 소켓을 닫는 메소드
        /// </summary>
        public void Close()
        {
            if(lis_sock!=null)
            {
                lis_sock.Close();
                lis_sock = null;
            }
        }

        /// <summary>
        /// 수신 메소드
        /// </summary>
        /// <param name="dosock"></param>
        private void Recevice(Socket dosock)
        {
            byte[] lbuf = new byte[4];  // 이미지 길이를 수신할 버퍼
            dosock.Receive(lbuf);   // 이미지 길이를 수신

            int len = BitConverter.ToInt32(lbuf, 0); // 버퍼의 내용을 정수형으로 변환
            byte[] buffer = new byte[len];  // 이미지 길이만큼의 버퍼 생성(이미지를 수신하는 버퍼)
            int trans = 0;
            while(trans<len)    // 수신할 데이터가 남아있을 경우 반복
            {
                trans += dosock.Receive(buffer, trans, len - trans, SocketFlags.None);
            }

            if(RecvedImage != null) // 이미지 수신 이벤트가 존재할 경우
            {
                IPEndPoint ep = dosock.RemoteEndPoint as IPEndPoint;
                RecvImageEventArgs e = new RecvImageEventArgs(ep, ConvertBitmap(buffer));
                RecvedImage(this, e);
            }
        }

        /// <summary>
        /// 수신한 버퍼를 비트맵 이미지로 전환하는 메소드
        /// </summary>
        /// <param name="data">수신한 데이터</param>
        /// <returns>비트맵 이미지</returns>
        public Bitmap ConvertBitmap(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, (int)data.Length);
            Bitmap bitmap = new Bitmap(ms);

            return bitmap;
        }
    }
}

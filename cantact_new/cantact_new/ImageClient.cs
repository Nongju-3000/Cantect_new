using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace cantact_new
{
    /// <summary>
    /// 이미지 선송 클라이언트
    /// </summary>
    public class ImageClient
    {
        Socket sock;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip"> 컨트롤러 IP</param>
        /// <param name="port">컨트롤러 포트</param>
        public void Connect(string ip, int port)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipaddr, port);
            sock.Connect(ep); // 연결 요청
        }

        /// <summary>
        /// 이미지 전송하는 메소드
        /// </summary>
        /// <param name="img">이미지</param>
        /// <returns>전송 여부</returns>
        public bool SendImage(Image img)
        {
            if(sock == null)    // 아무것도 없을때
            {
                return false;
            }
            MemoryStream ms = new MemoryStream();   // 메모리 스트림 생성
            img.Save(ms, ImageFormat.Jpeg); // 이미지를 Jpeg 로 생성된 메모리 스트림에 저장
            byte[] data = ms.GetBuffer();   // 버퍼를 가져옴
            try
            {
                int trans = 0;
                byte[] lbuf = BitConverter.GetBytes(data.Length);   // 기본 데이터 형식인 버퍼의 크기를 바이트의 배열로 변환
                sock.Send(lbuf);    // 바이트 배열 전송

                while(trans < data.Length)  // 전송한 크기가 배열의 크기보다 작을 경우 반복
                {
                    trans += sock.Send(data, trans, data.Length - trans, SocketFlags.None);
                }
                sock.Close();
                sock = null;
                return true;
            }

            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 이미지를 전송하는 메소드
        /// </summary>
        /// <param name="img">이미지</param>
        /// <param name="callback">이미지 전송이 완료됐을 경우 처리할 콜백</param>
        public void SendImageAsync(Image img, AsyncCallback callback)
        {
            SendImageDele dele = SendImage; 
            dele.BeginInvoke(img, callback, this);
        }

        /// <summary>
        /// 이미지 클라이언트를 닫아주는 메소드
        /// </summary>
        public void Close()
        {
            if(sock != null)
            {
                sock.Close();
                sock = null;
            }
        }
    }

    public delegate bool SendImageDele(Image img);
}


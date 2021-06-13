using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace cantact_new
{
    public enum MsgType // 원격 제어 이벤트들의 종류 (여기에 휠 이벤트랑 관련 메소드 추가해주세요)
    {
        MT_KDOWN=1, MT_KEYUP, MT_M_LEFTDOWN, MT_M_LEFTUP, MT_M_RIGHTDOWN,
        MT_M_RIGHTUP, MT_M_MIDDLEDOWN, MT_M_MIDDLEUP, MT_M_MOVE
    }

    /// <summary>
    /// 원격 제어 이벤트 클라이언트 클래스
    /// </summary>
    public class SendEventClient
    {
        IPEndPoint ep;
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="ip">호스트 IP</param>
        /// <param name="port">포트</param>
        public SendEventClient(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        /// <summary>
        /// 키 누름 메소드
        /// </summary>
        /// <param name="Key">키</param>
        public void SendKeyDown(int Key)
        {
            byte[] data = new byte[9];
            data[0] = (byte)MsgType.MT_KDOWN;
            Array.Copy(BitConverter.GetBytes(Key), 0, data, 1, 4);  // 입력된 키를 버퍼에 복사
            SendData(data); // 전송
        }

        private void SendData(byte[] data)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Connect(ep);
            sock.Send(data);
            sock.Close();
        }

        /// <summary>
        /// 키 복귀 메소드
        /// </summary>
        /// <param name="key">키</param>
        public void SendKeyUp(int key)
        {
            byte[] data = new byte[9];
            data[0] = (byte)MsgType.MT_KEYUP;
            Array.Copy(BitConverter.GetBytes(key), 0, data, 1, 4);
            SendData(data);
        }

        /// <summary>
        /// 마우스 누름 메소드
        /// </summary>
        /// <param name="mousebuttons">버튼</param>
        public void SendMouseDown(MouseButtons mousebuttons)
        {
            byte[] data = new byte[9];
            switch(mousebuttons)
            {
                case MouseButtons.Left: data[0] = (byte)MsgType.MT_M_LEFTDOWN; break;
                case MouseButtons.Right: data[0] = (byte)MsgType.MT_M_RIGHTDOWN; break;
                case MouseButtons.Middle: data[0] = (byte)MsgType.MT_M_MIDDLEDOWN; break;
            }
            SendData(data);
        }

        /// <summary>
        /// 마우스 복귀 메소드
        /// </summary>
        /// <param name="mousebuttons">버튼</param>
        public void SendMouseUp(MouseButtons mousebuttons)
        {
            byte[] data = new byte[9];
            switch (mousebuttons)
            {
                case MouseButtons.Left: data[0] = (byte)MsgType.MT_M_LEFTUP; break;
                case MouseButtons.Right: data[0] = (byte)MsgType.MT_M_RIGHTUP; break;
                case MouseButtons.Middle: data[0] = (byte)MsgType.MT_M_MIDDLEUP; break;
            }
            SendData(data);
        }
        /// <summary>
        /// 마우스 이동 메소드
        /// </summary>
        /// <param name="x">x좌표</param>
        /// <param name="y">y좌표</param>
        public void SendMouseMove(int x, int y)
        {
            byte[] data = new byte[9];
            data[0] = (byte)MsgType.MT_M_MOVE;
            Array.Copy(BitConverter.GetBytes(x), 0, data, 1, 4);
            Array.Copy(BitConverter.GetBytes(y), 0, data, 5, 4);
            SendData(data);
        }
    }
}

using RCEventArgsLib;
using System.Drawing;
using System.Windows.Automation;

namespace cantact_new
{
    /// <summary>
    /// 원격 제어 호스트 표현
    /// </summary>
    public class Remote
    {
        static Remote singleton;    // 단일체

        /// <summary>
        /// 단일 개체 가져오기
        /// </summary>
        public static Remote Singleton
        {
            get
            {
                return singleton;
            }
        }

        static Remote()
        {
            singleton = new Remote();   // 단일체 생성
        }

        /// <summary>
        /// 생성자
        /// </summary>
        private Remote()
        {
            // UI 자동화 기술을 이용하여 화명의 영역을 구함
            AutomationElement ae = AutomationElement.RootElement;
            System.Windows.Rect rt = ae.Current.BoundingRectangle;
            Rect = new Rectangle((int)rt.Left, (int)rt.Top, (int)rt.Width, (int)rt.Height);

            SetupServer.RecvedRCInfoEventHandler += SetupServer_RecvedRCInfoEventHandler;
            SetupServer.Start(MyIP, NetworkInfo.SetupPort);
        }

        /// <summary>
        /// IP 문자열
        /// </summary>
        public string MyIP
        {
            get
            {
                return NetworkInfo.DefaultIP;
            }
        }

        private void SetupServer_RecvedRCInfoEventHandler(object sender, RecvRCInfoEventArgs e)
        {
            RecvedRCInfo(this, e);
        }

        public event RecvRCInfoEventHandler RecvedRCInfo = null;
        public event RecvKMEEventHandler RecvedKMEvent = null;
        RecvEventServer res = null;

        /// <summary>
        /// 화면 영역
        /// </summary>
        public Rectangle Rect
        {
            get;
            private set;
        }

        /// <summary>
        /// 이벤트 수신 시작
        /// </summary>
        public void RecvEventStart()
        {
            res = new RecvEventServer(MyIP, NetworkInfo.EventPort);
            res.RecvedKMEvent += Res_RecvedKMEvent;
        }

        /// <summary>
        /// 키보드 마우스 이벤트 수신
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Res_RecvedKMEvent(object sender, RecvKMEEventArgs e)
        {
            if(RecvedKMEvent != null)
            {
                RecvedKMEvent(this, e);
            }
            // 수신한 이벤트에 따라 발생(여기에 휠 관련 부분 추가해주세요)
            switch(e.MT)
            {
                case MsgType.MT_KDOWN: WrapNative.KeyDown(e.Key); break;
                case MsgType.MT_KEYUP: WrapNative.KeyUp(e.Key); break;
                case MsgType.MT_M_LEFTDOWN: WrapNative.LeftDown(); break;
                case MsgType.MT_M_LEFTUP: WrapNative.LeftUp(); break;
                case MsgType.MT_M_RIGHTDOWN: WrapNative.RightDown(); break;
                case MsgType.MT_M_RIGHTUP: WrapNative.RightUp(); break;
                case MsgType.MT_M_MIDDLEDOWN: WrapNative.MiddleDown(); break;
                case MsgType.MT_M_MIDDLEUP: WrapNative.MiddleUp(); break;
                case MsgType.MT_M_MOVE: WrapNative.Move(e.Now); break;
            }
        }

        /// <summary>
        /// 서버 닫음
        /// </summary>
        public void Stop()
        {
            SetupServer.Close();
            if(res!=null)
            {
                res.Close();
                res = null;
            }
        }
    }
}

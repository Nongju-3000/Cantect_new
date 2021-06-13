using System;

namespace cantact_new
{
    /// <summary>
    /// 원격 제어 컨트롤러 클래스
    /// </summary>
    public class Controller
    {
        static Controller singleton; // 단일체 선언

        /// <summary>
        /// 단일체 접근자
        /// </summary>
        public static Controller Singleton
        {
            get
            {
                return singleton;
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        static Controller()
        {
            singleton = new Controller();
        }

        private Controller()
        {
        }

        ImageServer img_server = null;  // 이미지 수신 서버
        SendEventClient sce = null; // 이벤트 전송 클라이언트
        /// <summary>
        /// 이미지 수신 이벤트
        /// </summary>
        public event RecvImageEventHandler RecvedImage = null;  
        string host_ip; //IP 문자열

        /// <summary>
        /// 이벤트 전송 클라이언트 접근자
        /// </summary>
        public SendEventClient SendEventClient  
        {
            get
            {
                return sce;
            }
        }
        /// <summary>
        /// IP 문자열 접근자
        /// </summary>
        public string MyIP
        {
            get
            {
                return NetworkInfo.DefaultIP;
            }
        }

        /// <summary>
        /// 원격 컨트롤러 시작 메소드
        /// </summary>
        /// <param name="host_ip">호스트 IP주소</param>
        public void Start(string host_ip)
        {
            this.host_ip = host_ip;
            img_server = new ImageServer(MyIP, NetworkInfo.ImgPort);    // 이미지 서버
            img_server.RecvedImage += Img_server_RecvedImage;
            SetupClient.Setup(host_ip, NetworkInfo.SetupPort);  // 원격 제어 요청
        }

        /// <summary>
        /// 이벤트 클라이언트 시작 메소드
        /// </summary>
        public void StartEventClient()
        {
            sce = new SendEventClient(host_ip, NetworkInfo.EventPort);
        }

        /// <summary>
        /// 이미지 수신 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Img_server_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if(RecvedImage != null)
            {
                RecvedImage(this, e);
            }
        }

        /// <summary>
        /// 원격 컨트롤러 중지 메소드
        /// </summary>
        public void Stop()
        {
            if(img_server != null)
            {
                img_server.Close();
                img_server = null;
            }
        }
    }
}

using RCEventArgsLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace cantact_new
{
    public partial class MainForm : Form
    {
        string sip; // 상대의 IP
        int sport;  // 상대의 포트
        RemoteClientForm rcf = null;    // 제어 화면
        VirtualCursorForm vcf = null;   // 가상 커서

        public MainForm()
        {
            InitializeComponent();
        }


        private void btn_setting_Click(object sender, EventArgs e)
        {
            if(tbox_ip.Text == NetworkInfo.DefaultIP)   // 자신과 IP가 같을 경우
            {
                MessageBox.Show("자신을 원격 제어 할 수 없습니다.");  // 중지
                tbox_ip.Text = string.Empty;
                return;
            }
            string host_ip = tbox_ip.Text;
            Rectangle rect = Remote.Singleton.Rect;
            Controller.Singleton.Start(host_ip);    // 컨트롤러 시작

            rcf.ClientSize = new Size(rect.Width - 40, rect.Height - 80);
            rcf.Show(); // 원격 제어 화면 출력
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            Text += ":" + NetworkInfo.DefaultIP;    // 상단에 자신의 IP주소 표시
            vcf = new VirtualCursorForm();  // 가상 커서 생성
            rcf = new RemoteClientForm();   // 원격 조종화면 생성
            Remote.Singleton.RecvedRCInfo += Singleton_RecvedRCInfo;    // 원격 제어 요청 이벤트 핸들러 등록
        }

        delegate void Remote_Dele(object sender, RecvRCInfoEventArgs e);

        private void Singleton_RecvedRCInfo(object sender, RCEventArgsLib.RecvRCInfoEventArgs e)
        {
            if(this.InvokeRequired)
            {
                object[] objs = new object[2] { sender, e };
                this.Invoke(new Remote_Dele(Singleton_RecvedRCInfo), objs);
            }

            else
            {
                tbox_controller_ip.Text = e.IPAddressStr;   // IP주소 표시
                sip = e.IPAddressStr;   // IP 주소 설정
                sport = e.Port; // 요청 포트 설정
                btn_ok.Enabled = true;  // ok 버튼 활성화
            }
        }

        private void timer_send_image_Tick(object sender, EventArgs e)
        {
            Rectangle rect = Remote.Singleton.Rect; // 화면 사각 영역
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);    // 비트맵 생성
            Graphics graphics = Graphics.FromImage(bitmap); // Graphics 개체 생성

            Size size2 = new Size(rect.Width, rect.Height);
            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), size2);   // 화면 복사
            graphics.Dispose();

            try
            {
                ImageClient ic = new ImageClient();
                ic.Connect(sip, NetworkInfo.ImgPort);
                ic.SendImageAsync(bitmap, null);    // 이미지 비동기 전송
            }
            catch   // 연결 실패할 경우
            {
                timer_send_image.Stop();
                MessageBox.Show("서버에 문제가 발생했습니다.");
                this.Close();
            }
        }

        private void noti_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Hide();    // 자신의 화면을 숨겨버림(상대가 원격 조종하기 위해)
            Remote.Singleton.RecvEventStart();  // 원격 제어 이벤트 시작
            timer_send_image.Start();   // 이미지 전송 타이머 시작
            vcf.Show(); // 가상 커서 출력
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Remote.Singleton.Stop();    // 원격 멈춤
            Controller.Singleton.Stop();    // 컨트롤러 멈춤
            Application.Exit(); // 종료
        }
    }
}

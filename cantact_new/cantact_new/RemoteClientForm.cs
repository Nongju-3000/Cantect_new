using System;
using System.Drawing;
using System.Windows.Forms;

namespace cantact_new
{
    /// <summary>
    /// 원격 조종 클라이언트
    /// </summary>
    public partial class RemoteClientForm : Form
    {
        bool check; // 이미지 수신 여부
        Size csize; // 클라이언트 크기
        SendEventClient EventSC
        {
            get
            {
                return Controller.Singleton.SendEventClient;
            }
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public RemoteClientForm()
        {
            InitializeComponent();
        }

        private void RemoteClientForm_Load(object sender, EventArgs e)
        {
            Controller.Singleton.RecvedImage += Singleton_RecvedImage;
        }

        /// <summary>
        /// 이미지 수신 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Singleton_RecvedImage(object sender, RecvImageEventArgs e)
        {
            if(check == false)
            {
                Controller.Singleton.StartEventClient();
                check = true;
                csize = e.Image.Size;
            }
            pbox_remote.Image = e.Image;
        }

        /// <summary>
        /// 키 복귀 이벤트 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteClientForm_KeyUp(object sender, KeyEventArgs e)
        {
            if(check == true)
            {
                EventSC.SendKeyUp(e.KeyValue);
            }
        }

        /// <summary>
        /// 키 누름 이벤트 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteClientForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(check==true)
            {
                EventSC.SendKeyDown(e.KeyValue);
            }

        }
        
        /// <summary>
        /// 마우스 누름 이벤트 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbox_remote_MouseDown(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseDown(e.Button);
            }
        }

        /// <summary>
        /// 마우스 이동 이벤트 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbox_remote_MouseMove(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Point pt = ConvertPoint(e.X, e.Y);
                EventSC.SendMouseMove(pt.X,pt.Y);
            }
        }

        /// <summary>
        /// 좌표 이동 메소드
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Point ConvertPoint(int x, int y)
        {
            int nx = csize.Width * x / pbox_remote.Width;
            int ny = csize.Height * y / pbox_remote.Height;
            return new Point(nx, ny);
        }

        /// <summary>
        /// 마우스 버튼 복귀 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbox_remote_MouseUp(object sender, MouseEventArgs e)
        {
            if (check == true)
            {
                Text = e.Location.ToString();
                EventSC.SendMouseUp(e.Button);
            }
        }
    }
}

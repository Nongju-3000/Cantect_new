using System;
using System.Drawing;
using System.Windows.Forms;

namespace cantact_new
{
    /// <summary>
    /// 가상 커서
    /// </summary>
    public partial class VirtualCursorForm : Form
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public VirtualCursorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 커서 로드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VirtualCursorForm_Load(object sender, EventArgs e)
        {
            Size = new Size(10, 10);    // 사이즈 10*10 지정
            Remote.Singleton.RecvedKMEvent += Singleton_RecvedKMEvent;
        }

       /// <summary>
       /// 커서 위치 지정
       /// </summary>
       /// <param name="now">커서 좌표</param>
       /// <param name="mt">마우스 이동 이벤트</param>
        void ChangeLocation(Point now, MsgType mt)
        {
            if(mt == MsgType.MT_M_MOVE)
            {
                Location = new Point(now.X + 2, now.Y + 2);
            }
        }

        // 크로스 스레드 방지를 위한 부분
        delegate void ChangeLocationDele(Point now, MsgType mt);

        private void Singleton_RecvedKMEvent(object sender, RecvKMEEventArgs e)
        {
            if(this.InvokeRequired)
            {
                object[] objs = new object[] { e.Now, e.MT };
                this.Invoke(new ChangeLocationDele(ChangeLocation), objs);
            }
            else
            {
                ChangeLocation(e.Now, e.MT);
            }
        }
    }
}

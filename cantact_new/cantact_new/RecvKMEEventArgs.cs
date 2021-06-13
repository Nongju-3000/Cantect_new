using System;
using System.Drawing;

namespace cantact_new
{
    /// <summary>
    /// 대리자
    /// </summary>
    /// <param name="sender">이벤트를 통보하는 개체</param>
    /// <param name="e">인자</param>
    public delegate void RecvKMEEventHandler(object sender, RecvKMEEventArgs e);

    /// <summary>
    /// 원격 제어 이벤트 수신 여부를 통보하는 클래스
    /// </summary>
    public class RecvKMEEventArgs:EventArgs
    {
        /// <summary>
        /// 수신정보를 변환한 개체 
        /// </summary>
        public Meta Meta
        {
            get;
            private set;
        }

        /// <summary>
        /// 키보드 키
        /// </summary>
        public int Key
        {
            get
            {
                return Meta.Key;
            }
        }

        /// <summary>
        /// 마우스 좌표
        /// </summary>
        public Point Now
        {
            get
            {
                return Meta.Now;
            }
        }

        /// <summary>
        /// 이벤트 종류
        /// </summary>
        public MsgType MT
        {
            get
            {
                return Meta.MT;
            }
        }
        public RecvKMEEventArgs(Meta meta)
        {
            Meta = meta;
        }
    }
}

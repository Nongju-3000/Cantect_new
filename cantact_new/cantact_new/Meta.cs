using System.Drawing;

namespace cantact_new
{
    /// <summary>
    /// 원격 제어 이벤트를 수신정보를 변환하는 클래스
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// 이벤트 종류
        /// </summary>
        public MsgType MT
        {
            get;
            private set;
        }
        /// <summary>
        /// 키
        /// </summary>
        public int Key
        {
            get;
            private set;
        }
        /// <summary>
        /// 마우스 좌표
        /// </summary>
        public Point Now
        {
            get;
            private set;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="data">수신한 원격 제어 데이터</param>
        public Meta(byte[] data)
        {
            MT = (MsgType)data[0];
            switch(MT)
            {
                case MsgType.MT_KDOWN:
                case MsgType.MT_KEYUP:
                    MakingKey(data); break; // 수신한 데이터를 키로 변환
                case MsgType.MT_M_MOVE:
                    MakingPoint(data); break; // 수신한 데이터를 좌표로 변환
            }
        }

        /// <summary>
        /// data를 좌표로 변환하는 메소드
        /// </summary>
        /// <param name="data">원격 제어 수신한 데이터</param>
        private void MakingPoint(byte[] data)
        {
            Point now = new Point(0, 0);
            now.X = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + data[1];
            now.Y = (data[8] << 24) + (data[7] << 16) + (data[6] << 8) + data[5];

            Now = now;
        }

        /// <summary>
        /// data를 키로 변환하는 메소드
        /// </summary>
        /// <param name="data">원격 제어 수신한 키</param>
        private void MakingKey(byte[] data)
        {
            Key = (data[4] << 24) + (data[3] << 16) + (data[2] << 8) + data[1];
        }
    }
}

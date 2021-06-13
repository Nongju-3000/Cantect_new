using System.Net;
using System.Net.Sockets;

namespace cantact_new
{
    /// <summary>
    /// 네트워크 정보 클래스
    /// </summary>
    /// 포트들은 잘 사용되지 않는 포트들로 지정했습니다
    public static class NetworkInfo
    {
        /// <summary>
        /// 이미지 서버의 포트
        /// </summary>
        public static short ImgPort
        {
            get
            {
                return 20005;
            }
        }

        /// <summary>
        /// 원격 제어 요청 포트
        /// </summary>
        public static short SetupPort
        {
            get
            {
                return 20002;
            }
        }

        /// <summary>
        /// 원격 제어 이벤트 포트
        /// </summary>
        public static short EventPort
        {
            get
            {
                return 20011;
            }
        }

        /// <summary>
        /// IP주소 문자열 메소드
        /// </summary>
        public static string DefaultIP
        {
            get
            {
                // 호스트의 이름
                string host_name = Dns.GetHostName();
                // 호스트 엔트리
                IPHostEntry host_entry = Dns.GetHostEntry(host_name);
                // 호스트 주소 반복
                foreach(IPAddress ipaddr in host_entry.AddressList)
                {
                    // 만약 주소가 InterNetwork 체계일 때
                    if(ipaddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        //ip 주소 문자열을 반환함
                        return ipaddr.ToString();
                    }
                }
                return "127.0.0.1"; //루프백 ip 반환
            }
        }
    }
}
